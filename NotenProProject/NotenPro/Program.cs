using HTLKrems.GradeManagement;
using HTLKrems.GradeManagement.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 🔐 MICROSOFT IDENTITY
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.UserOptions.RoleClaim = "roles";
});

// 🔥 NUR EINEN HttpClient MIT BaseAddress
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/");
    Console.WriteLine($"🌐 ApiClient BaseAddress: {client.BaseAddress}");
})
.AddHttpMessageHandler(sp =>
{
    var handler = sp.GetRequiredService<AuthorizationMessageHandler>();
    handler.ConfigureHandler(authorizedUrls: new[] { "https://localhost:5001/" });
    return handler;
});

// 📦 ALLE SERVICES BEKOMMEN DENSELBEN CLIENT
builder.Services.AddScoped<ICurrentUserService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("ApiClient");
    return new CurrentUserApiService(httpClient);
});

builder.Services.AddScoped<IGradeService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var currentUserService = sp.GetRequiredService<ICurrentUserService>();
    return new GradeApiService(httpClientFactory, currentUserService);
});

builder.Services.AddScoped<IStudentService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var currentUserService = sp.GetRequiredService<ICurrentUserService>();
    return new StudentApiService(httpClientFactory, currentUserService);
});

builder.Services.AddScoped<INotificationService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var currentUserService = sp.GetRequiredService<ICurrentUserService>();
    return new NotificationApiService(httpClientFactory, currentUserService);
});

builder.Services.AddMudServices();

await builder.Build().RunAsync();