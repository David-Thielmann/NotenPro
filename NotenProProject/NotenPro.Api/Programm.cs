// Program.cs
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotenPro.Api.Data;
using System.Text.Json.Serialization;
using HTLKrems.GradeManagement.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<NotenProDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 33))
    ));

// 🔥 PDF EXPORT SERVICE REGISTRIEREN
builder.Services.AddScoped<IPdfExportService, PdfExportService>();

// 🔥 AUTHENTICATION AKTIV!
var azureAdConfig = builder.Configuration.GetSection("AzureAd");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"{azureAdConfig["Instance"]}{azureAdConfig["TenantId"]}/v2.0";
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"{azureAdConfig["Instance"]}{azureAdConfig["TenantId"]}/v2.0",
            ValidateAudience = false, // 🔥 FALSE für Development!
            ValidAudience = null,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
        
        // Debug Events
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                Console.WriteLine($"✅ TOKEN VALIDATED");
                Console.WriteLine($"User: {context.Principal?.Identity?.Name}");
                Console.WriteLine($"OID: {context.Principal?.FindFirst("oid")?.Value}");
                Console.WriteLine($"Authenticated: {context.Principal?.Identity?.IsAuthenticated}");
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"❌ AUTH FAILED: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine($"⚠️ CHALLENGE: {context.Error}");
                Console.WriteLine($"Header: {context.Request.Headers["Authorization"]}");
                return Task.CompletedTask;
            }
        };
        
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.WithOrigins(
                "https://localhost:5000",
                "http://localhost:5000",
                "https://localhost:7000",
                "http://localhost:7000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorApp");

// 🔥 MIDDLEWARE AKTIV!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();