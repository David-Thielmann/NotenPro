using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MudBlazor.Services;
using HTLKrems.GradeManagement;
using HTLKrems.GradeManagement.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 🌐 Standard HttpClient (für Blazor intern)
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
);

// 🔐 Microsoft Entra ID (MSAL)
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);

    // 🔴 DAS war der Schlüssel für Roles
    options.UserOptions.RoleClaim = "roles";
});

// 🌐 HttpClient → API (mit Access Token)
builder.Services.AddHttpClient("NotenProApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7001/");
})
.AddHttpMessageHandler(sp =>
{
    var handler = sp.GetRequiredService<AuthorizationMessageHandler>()
        .ConfigureHandler(
            authorizedUrls: new[] { "https://localhost:7001/" },
            scopes: new[] { "api://03f0164a-e673-4862-9ef7-2ac41b743329/access_as_user" }
        );

    return handler;
});

//Koffer api NotenProApi
builder.Services.AddHttpClient("NotenProApi", client =>
    {
        var baseUrl = builder.Configuration["Api:BaseUrl"]!;
        client.BaseAddress = new Uri(baseUrl);
    })
    .AddHttpMessageHandler(sp =>
    {
        var baseUrl = builder.Configuration["Api:BaseUrl"]!;
        return sp.GetRequiredService<AuthorizationMessageHandler>()
            .ConfigureHandler(
                authorizedUrls: new[] { baseUrl },
                scopes: new[] { "api://03f0164a-e673-4862-9ef7-2ac41b743329/access_as_user" }
            );
    });



// 🎨 MudBlazor
builder.Services.AddMudServices();

// 📦 Application Services
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<IPdfExportService, PdfExportService>();
//api service
builder.Services.AddScoped<ICurrentUserService, CurrentUserApiService>();
builder.Services.AddScoped<IGradeService, GradeApiService>();
builder.Services.AddScoped<INotificationService, NotificationApiService>();
builder.Services.AddScoped<IStudentService, StudentApiService>();



await builder.Build().RunAsync();
