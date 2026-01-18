using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using HTLKrems.GradeManagement;
using HTLKrems.GradeManagement.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 🧩 MudBlazor services (required for MudDialogProvider / IDialogService)
builder.Services.AddMudServices();

// 🔐 MSAL (Azure AD) – Login bleibt aktiv (ID token), aber wir benötigen kein Access Token für die API.
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);

    // DefaultScopes können vorhanden sein, sind aber für die DEV-API-Aufrufe nicht nötig.
    var scopes = builder.Configuration.GetSection("AzureAd:DefaultScopes").Get<string[]>() ?? Array.Empty<string>();
    foreach (var s in scopes)
        options.ProviderOptions.DefaultAccessTokenScopes.Add(s);
});

// 🌐 Named HttpClient (DEV): sendet User-Claims als Header, kein Bearer Token erforderlich
builder.Services.AddTransient<OidHeaderHandler>();

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"] ?? "https://localhost:5001/");
})
.AddHttpMessageHandler<OidHeaderHandler>();

// Standard HttpClient NICHT für API verwenden!
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

// Deine Services müssen genau diesen Client bekommen
builder.Services.AddScoped<ICurrentUserService, CurrentUserApiService>();
builder.Services.AddScoped<IStudentService, StudentApiService>();
builder.Services.AddScoped<IGradeService, GradeApiService>();
builder.Services.AddScoped<INotificationService, NotificationApiService>();

// Teacher / Stammdaten
builder.Services.AddScoped<ITeacherService, TeacherApiService>();
builder.Services.AddScoped<ITestService, TestApiService>();
builder.Services.AddScoped<IClassService, ClassApiService>();
builder.Services.AddScoped<IUserService, UserApiService>();
builder.Services.AddScoped<ISubjectService, SubjectApiService>();
builder.Services.AddScoped<IEarlyWarningService, EarlyWarningApiService>();

//IPdf export
builder.Services.AddScoped<IPdfExportService, PdfExportService>();

//Admin Dash 
builder.Services.AddScoped<IAdminDashboardService, AdminDashboardApiService>();




await builder.Build().RunAsync();
