using HTLKrems.GradeManagement.Api.Services;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------
// ConnectionString aus Config holen & debuggen
// ----------------------------------------------------

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not found in configuration."
    );
}

Console.WriteLine("=== DB CONNECTION STRING USED BY API ===");
Console.WriteLine(connectionString);
Console.WriteLine("========================================");

// ----------------------------------------------------
// Services registrieren
// ----------------------------------------------------

// Controller (klassische Web-API)
builder.Services.AddControllers();
builder.Services.AddScoped<IPdfExportService, PdfExportService>();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext konfigurieren (MySQL, Pomelo)
builder.Services.AddDbContext<NotenProDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        // feste Version statt AutoDetect, damit die Verbindung
        // nicht schon beim Erkennen der Version abraucht
        new MySqlServerVersion(new Version(8, 4, 2)) // ggf. an deine Version anpassen
    );
});

// CORS – Blazor-Client erlauben (für Entwicklung erstmal offen)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            // TODO: im echten Betrieb auf konkrete Origins einschränken (z.B. http://localhost:5000)
            .SetIsOriginAllowed(_ => true);
    });
});

var app = builder.Build();

// ----------------------------------------------------
// HTTP-Pipeline konfigurieren
// ----------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS vor Authorization/Endpoints
app.UseCors("AllowBlazorClient");

// (Optional: später Authentication einhängen)
// app.UseAuthentication();

app.UseAuthorization();

// Attribute-Routing der Controller aktivieren
app.MapControllers();

app.Run();
