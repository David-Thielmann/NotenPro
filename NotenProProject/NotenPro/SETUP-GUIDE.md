# Setup-Anleitung für HTL Krems Notenverwaltung

## Schnellstart (5 Minuten)

### Option 1: Neues Projekt erstellen

```bash
# 1. Neues Blazor WebAssembly Projekt
dotnet new blazorwasm -o HTLKrems.GradeManagement
cd HTLKrems.GradeManagement

# 2. MudBlazor installieren
dotnet add package MudBlazor

# 3. Alle Dateien aus blazor-export kopieren (manuell)

# 4. Projekt starten
dotnet run
```

### Option 2: Vorhandenes Projekt aktualisieren

```bash
# 1. MudBlazor installieren
dotnet add package MudBlazor

# 2. Dateien aus blazor-export überschreiben:
#    - Program.cs
#    - App.razor
#    - _Imports.razor
#    - wwwroot/index.html
#    - wwwroot/css/app.css

# 3. Neue Ordner hinzufügen:
#    - Models/
#    - Services/
#    - Pages/Student/, Pages/Teacher/, Pages/Admin/
#    - Shared/ (StudentLayout.razor)

# 4. Projekt starten
dotnet run
```

---

## Detaillierte Anleitung

### 1. Prerequisites installieren

#### Windows
```powershell
# .NET 8 SDK installieren
winget install Microsoft.DotNet.SDK.8

# Optional: Visual Studio 2022 mit Blazor Workload
# oder VS Code mit C# Extension
```

#### macOS
```bash
# .NET 8 SDK installieren
brew install --cask dotnet-sdk
```

#### Linux
```bash
# .NET 8 SDK installieren
wget https://dot.net/v1/dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 8.0
```

### 2. Projekt erstellen

```bash
# Ordner erstellen und wechseln
mkdir HTLKrems.GradeManagement
cd HTLKrems.GradeManagement

# Blazor WebAssembly Projekt erstellen
dotnet new blazorwasm
```

### 3. Packages installieren

```bash
# MudBlazor
dotnet add package MudBlazor --version 7.0.0
```

### 4. Dateistruktur erstellen

```bash
# Ordner erstellen (Windows PowerShell oder Linux/macOS)
mkdir Models
mkdir Services
mkdir Pages/Student
mkdir Pages/Teacher
mkdir Pages/Admin
mkdir Pages/SysAdmin
mkdir Shared
```

### 5. Dateien kopieren

#### Core-Dateien (ersetzen)
- ✅ `Program.cs`
- ✅ `App.razor`
- ✅ `_Imports.razor`
- ✅ `wwwroot/index.html`
- ✅ `wwwroot/css/app.css`
- ✅ `HTLKrems.GradeManagement.csproj`

#### Neue Dateien hinzufügen

**Models:**
- `Models/Models.cs`

**Services:**
- `Services/AuthService.cs`
- `Services/GradeService.cs`
- `Services/AllServices.cs`

**Pages:**
- `Pages/Login.razor`
- `Pages/Student/Dashboard.razor`
- `Pages/Student/Grades.razor`
- `Pages/Student/Notifications.razor`
- `Pages/Teacher/Dashboard.razor`
- `Pages/Admin/Dashboard.razor`
- `Pages/Admin/Teachers.razor`

**Shared:**
- `Shared/MainLayout.razor` (ersetzen)
- `Shared/StudentLayout.razor` (neu)

### 6. Projekt builden

```bash
dotnet build
```

Wenn Fehler auftreten:
```bash
# Dependencies wiederherstellen
dotnet restore

# Cache löschen
dotnet clean
dotnet build
```

### 7. Projekt starten

```bash
dotnet run
```

Oder mit Hot Reload:
```bash
dotnet watch run
```

### 8. Anwendung öffnen

Browser öffnen:
- **HTTPS**: `https://localhost:5001`
- **HTTP**: `http://localhost:5000`

---

## Troubleshooting

### Problem: "MudBlazor not found"

**Lösung:**
```bash
dotnet add package MudBlazor
dotnet restore
```

### Problem: "Cannot find namespace HTLKrems.GradeManagement.Models"

**Lösung:**
Stelle sicher, dass die `.csproj` Datei korrekt ist:
```xml
<Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <RootNamespace>HTLKrems.GradeManagement</RootNamespace>
  </PropertyGroup>
</Project>
```

### Problem: Hot Reload funktioniert nicht

**Lösung:**
```bash
# Alt + Ctrl + C zum Stoppen
dotnet watch run
```

### Problem: Seite lädt nicht / weiße Seite

**Lösung:**
1. Browser-Konsole öffnen (F12)
2. Fehler prüfen
3. Cache leeren (Ctrl + Shift + R)
4. Projekt neu builden

### Problem: "Route not found"

**Lösung:**
Stelle sicher, dass:
- `@page "/route"` am Anfang der .razor Datei steht
- Die Datei im richtigen Ordner ist
- `App.razor` korrekt ist

---

## IDE Setup

### Visual Studio 2022
1. Projekt öffnen: `File > Open > Project/Solution`
2. `HTLKrems.GradeManagement.csproj` auswählen
3. F5 zum Starten mit Debugging
4. Ctrl + F5 zum Starten ohne Debugging

### VS Code
1. Ordner öffnen: `File > Open Folder`
2. C# Extension installieren (ms-dotnettools.csharp)
3. F5 zum Starten
4. Terminal: `dotnet watch run`

### JetBrains Rider
1. Projekt öffnen
2. F5 zum Starten
3. Rechtsklick auf Projekt > Run

---

## Nächste Schritte nach dem Setup

### 1. Demo ausprobieren
- Login-Seite öffnen
- Verschiedene Rollen testen
- UI erkunden

### 2. Code verstehen
- `Services/` → Business Logik & Mock-Daten
- `Models/` → Datenmodelle
- `Pages/` → UI-Komponenten
- `Shared/` → Layouts

### 3. Erste Änderung machen
```razor
<!-- Pages/Student/Dashboard.razor -->
<MudText Typo="Typo.h4" Class="mb-6">
    Willkommen, @userName! <!-- Hinzufügen -->
</MudText>

@code {
    private string userName = "Max";
}
```

### 4. Neue Seite erstellen
```bash
# Neue Datei: Pages/Student/Profile.razor
```

```razor
@page "/student/profile"

<PageTitle>Profil</PageTitle>

<MudText Typo="Typo.h4">Mein Profil</MudText>

@code {
    protected override void OnInitialized()
    {
        // Initialisierung
    }
}
```

### 5. Backend vorbereiten
- ASP.NET Core Web API Projekt erstellen
- Entity Framework Core einrichten
- Controllers implementieren
- Blazor HttpClient auf API zeigen lassen

---

## Deployment

### Option 1: Azure Static Web Apps
```bash
# Azure CLI
az staticwebapp create \
  --name htl-krems-grades \
  --resource-group my-resource-group \
  --source . \
  --location "westeurope" \
  --app-location "/" \
  --output-location "wwwroot"
```

### Option 2: GitHub Pages
```bash
# 1. Base path in index.html setzen
<base href="/repository-name/" />

# 2. Publish
dotnet publish -c Release

# 3. Deploy wwwroot nach gh-pages branch
```

### Option 3: Docker
```dockerfile
# Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app

FROM nginx:alpine
COPY --from=build /app/wwwroot /usr/share/nginx/html
```

---

## Support & Ressourcen

- 📚 [MudBlazor Docs](https://mudblazor.com/)
- 📘 [Blazor Docs](https://learn.microsoft.com/aspnet/core/blazor/)
- 💬 [MudBlazor Discord](https://discord.gg/mudblazor)
- 🐛 [GitHub Issues](https://github.com/MudBlazor/MudBlazor/issues)

---

Viel Erfolg! 🚀
