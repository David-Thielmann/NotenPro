# HTL Krems Notenverwaltung - Blazor Export

## 📦 Vollständiger Export-Inhalt

Dieser Ordner enthält eine **funktionsfähige Blazor WebAssembly Anwendung** mit MudBlazor, die aus dem React/TypeScript-Prototypen konvertiert wurde.

---

## 📁 Dateiübersicht

### ✅ Kern-Dateien (ESSENTIAL)

| Datei | Beschreibung | Status |
|-------|--------------|---------|
| `HTLKrems.GradeManagement.csproj` | Projektdatei mit Dependencies | ✅ Fertig |
| `Program.cs` | App-Entry Point, Service-Registrierung | ✅ Fertig |
| `App.razor` | Root-Komponente, Router | ✅ Fertig |
| `_Imports.razor` | Globale Using-Statements | ✅ Fertig |

### 🎨 Layout & Design

| Datei | Beschreibung | Status |
|-------|--------------|---------|
| `Shared/MainLayout.razor` | Haupt-Layout (Sidebar, Header) | ✅ Fertig |
| `Shared/StudentLayout.razor` | Schüler-spezifisches Layout (Mobile) | ✅ Fertig |
| `wwwroot/index.html` | HTML-Template | ✅ Fertig |
| `wwwroot/css/app.css` | Custom CSS (HTL Krems Theme) | ✅ Fertig |

### 📊 Datenmodelle

| Datei | Beschreibung | Modelle |
|-------|--------------|---------|
| `Models/Models.cs` | Alle Datenmodelle | User, Grade, Test, Teacher, Class, Subject, School, Notification, DTOs, Enums |

### 🔧 Services (Backend-Logik)

| Datei | Interface | Mock-Daten |
|-------|-----------|------------|
| `Services/AuthService.cs` | `IAuthService` | 4 Demo-User |
| `Services/GradeService.cs` | `IGradeService` | 5 Mock-Grades |
| `Services/AllServices.cs` | 7+ Interfaces | Notifications, Tests, Teachers, Classes, Subjects, Schools, Students |

**Enthaltene Services:**
- AuthService: Login/Logout
- GradeService: Notenverwaltung
- NotificationService: Benachrichtigungen
- StudentService: Schüler-Dashboard-Stats
- TestService: Test-Verwaltung
- TeacherService: Lehrerverwaltung
- ClassService: Klassenverwaltung
- SubjectService: Fächerverwaltung
- SchoolService: Schulenverwaltung

### 📄 Seiten (Pages)

#### Login
- ✅ `Pages/Login.razor` - Login mit 4 Demo-Rollen

#### Schüler (Student) - 3/4 Seiten
- ✅ `Pages/Student/Dashboard.razor` - Dashboard mit Statistiken
- ✅ `Pages/Student/Grades.razor` - Notenübersicht mit Filter
- ✅ `Pages/Student/Notifications.razor` - Benachrichtigungen
- ❌ `Pages/Student/Map.razor` - Raumfinder (TODO)

#### Lehrer (Teacher) - 1/5 Seiten
- ✅ `Pages/Teacher/Dashboard.razor` - Dashboard mit Quick-Actions
- ❌ `Pages/Teacher/Tests.razor` - Test-Verwaltung (TODO)
- ❌ `Pages/Teacher/GradeEntry.razor` - Noteneintragung (TODO)
- ❌ `Pages/Teacher/Warnings.razor` - Frühwarnungen (TODO)
- ❌ `Pages/Teacher/Classes.razor` - Klassenübersicht (TODO)

#### Schuladministrator (Admin) - 2/7 Seiten
- ✅ `Pages/Admin/Dashboard.razor` - Dashboard mit Statistiken
- ✅ `Pages/Admin/Teachers.razor` - Lehrerverwaltung (CRUD)
- ❌ `Pages/Admin/Classes.razor` - Klassenverwaltung (TODO)
- ❌ `Pages/Admin/Subjects.razor` - Fächerverwaltung (TODO)
- ❌ `Pages/Admin/Maps.razor` - Raumplan-Upload (TODO)
- ❌ `Pages/Admin/Backups.razor` - Backup-Verwaltung (TODO)
- ❌ `Pages/Admin/Audit.razor` - Audit-Log (TODO)

#### Systemverwalter (SysAdmin) - 0/5 Seiten
- ❌ `Pages/SysAdmin/Dashboard.razor` - System-Dashboard (TODO)
- ❌ `Pages/SysAdmin/Schools.razor` - Schulenverwaltung (TODO)
- ❌ `Pages/SysAdmin/Admins.razor` - Admin-Verwaltung (TODO)
- ❌ `Pages/SysAdmin/Monitoring.razor` - System-Monitoring (TODO)
- ❌ `Pages/SysAdmin/Settings.razor` - Einstellungen (TODO)

### 📚 Dokumentation

| Datei | Beschreibung |
|-------|--------------|
| `README.md` | Haupt-Dokumentation |
| `SETUP-GUIDE.md` | Detaillierte Setup-Anleitung |
| `IMPLEMENTATION-STATUS.md` | Implementierungs-Status & TODOs |
| `INDEX.md` | Diese Datei - Übersicht |

---

## 🎯 Fertigstellungsgrad

### Gesamt: ~30%

- ✅ **Core-Setup**: 100% (Projekt, Services, Models, Layout)
- ✅ **Schüler-Ansicht**: 75% (3/4 Seiten)
- ⚠️ **Lehrer-Ansicht**: 20% (1/5 Seiten)
- ⚠️ **Admin-Ansicht**: 29% (2/7 Seiten)
- ❌ **SysAdmin-Ansicht**: 0% (0/5 Seiten)

### Was funktioniert bereits:
- ✅ Login mit 4 verschiedenen Rollen
- ✅ Rollenbasierte Navigation
- ✅ Responsive Design (Desktop/Tablet/Mobile)
- ✅ Mobile Bottom-Navigation (Schüler)
- ✅ Schüler kann Noten sehen und filtern
- ✅ Schüler kann Benachrichtigungen verwalten
- ✅ Lehrer sieht Dashboard mit Statistiken
- ✅ Admin kann Lehrer verwalten (CRUD)
- ✅ HTL Krems Theme (MudBlazor)
- ✅ Toast-Benachrichtigungen
- ✅ Dialoge für CRUD-Operationen

### Was noch fehlt:
- ❌ Test-Verwaltung (Lehrer)
- ❌ Noteneintragung (Lehrer)
- ❌ Frühwarnungen (Lehrer)
- ❌ Klassen-/Fächer-Verwaltung (Admin)
- ❌ Backup & Audit-Log (Admin)
- ❌ Komplette SysAdmin-Ansicht
- ❌ Backend-API Integration
- ❌ Datenbank-Anbindung

---

## 🚀 Schnellstart

### 1. Installation
```bash
# .NET 8 SDK installiert?
dotnet --version

# Projekt erstellen
dotnet new blazorwasm -o HTLKrems.GradeManagement
cd HTLKrems.GradeManagement

# MudBlazor installieren
dotnet add package MudBlazor
```

### 2. Dateien kopieren
Alle Dateien aus `blazor-export/` in Ihr Projekt kopieren

### 3. Starten
```bash
dotnet run
```

### 4. Login
Browser öffnen: `https://localhost:5001`

**Demo-Zugänge:**
- Schüler: `student@htl-krems.at` / `student`
- Lehrer: `teacher@htl-krems.at` / `teacher`
- Admin: `admin@htl-krems.at` / `admin`
- SysAdmin: `sysadmin@htl-krems.at` / `sysadmin`

---

## 📋 Was ist implementiert?

### ✅ Vollständig funktionsfähig

#### Login-System
- 4 verschiedene Rollen
- Passwort-Anzeige Toggle
- Quick-Login Buttons
- Rollenbasierte Weiterleitung

#### Schüler-Dashboard
- 4 Statistik-Karten (Durchschnitt, Tests, Benachrichtigungen, Klasse)
- Letzte 5 Noten
- Fächer-Übersicht mit Durchschnitt

#### Schüler-Notenübersicht
- Komplette Notenliste
- Filter nach Fach
- Suche
- Pagination
- Farbcodierung (Grün/Orange/Rot)
- Export-Button (UI)

#### Schüler-Benachrichtigungen
- Liste aller Benachrichtigungen
- Ungelesen-Badge
- Als gelesen markieren
- Typ-Icons (Info/Warning/Success/Error)

#### Lehrer-Dashboard
- 4 Statistik-Karten
- Schnellaktionen-Buttons
- Anstehende Tests

#### Admin-Lehrerverwaltung
- Tabelle mit Suche
- Lehrer hinzufügen (Dialog)
- Lehrer löschen (mit Bestätigung)
- Status-Badges
- Filter-Funktion

#### Layouts
- Desktop: Sidebar-Navigation
- Mobile (Schüler): Bottom-Navigation mit Badges
- Responsive Header mit User-Menu
- HTL Krems Theme

---

## 🛠️ Technologie-Stack

- **Framework**: Blazor WebAssembly (.NET 8)
- **UI Library**: MudBlazor 7.0
- **Sprache**: C# 12
- **Styling**: MudBlazor Theme + Custom CSS
- **State Management**: Scoped Services
- **Routing**: Blazor Router
- **Icons**: Material Icons (MudBlazor)

---

## 📖 Wie weiter entwickeln?

### Neue Seite hinzufügen (Beispiel: Teacher/Tests.razor)

1. **Datei erstellen**: `Pages/Teacher/Tests.razor`

```razor
@page "/teacher/tests"
@inject ITestService TestService

<PageTitle>Meine Tests</PageTitle>

<MudText Typo="Typo.h4" Class="mb-6">Meine Tests</MudText>

<MudTable Items="@tests" Hover="true">
    <HeaderContent>
        <MudTh>Name</MudTh>
        <MudTh>Fach</MudTh>
        <MudTh>Datum</MudTh>
    </HeaderContent>
    <RowTemplate>
        <MudTd>@context.Name</MudTd>
        <MudTd>@context.Subject</MudTd>
        <MudTd>@context.Date.ToString("dd.MM.yyyy")</MudTd>
    </RowTemplate>
</MudTable>

@code {
    private List<Test> tests = new();

    protected override async Task OnInitializedAsync()
    {
        tests = await TestService.GetMyTestsAsync();
    }
}
```

2. **Navigation hinzufügen** in `Shared/MainLayout.razor`:
```razor
<MudNavLink Href="/teacher/tests" Icon="@Icons.Material.Filled.Assignment">
    Tests
</MudNavLink>
```

3. **Fertig!** Seite ist jetzt erreichbar.

---

## 🔄 Von Mock-Daten zu echtem Backend

### Aktuell (Mock):
```csharp
// Services/GradeService.cs
public Task<List<Grade>> GetMyGradesAsync()
{
    return Task.FromResult(_mockGrades);
}
```

### Production (API):
```csharp
public class GradeService : IGradeService
{
    private readonly HttpClient _http;

    public GradeService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Grade>> GetMyGradesAsync()
    {
        var response = await _http.GetFromJsonAsync<ApiResponse<List<Grade>>>(
            "api/grades/me"
        );
        return response?.Data ?? new List<Grade>();
    }
}
```

Dann in `Program.cs`:
```csharp
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("https://your-api.com") 
});
```

---

## 💡 Tipps & Best Practices

### 1. MudBlazor Komponenten nutzen
```razor
<!-- Statt div + CSS -->
<MudCard Elevation="2">
    <MudCardContent>
        <!-- Content -->
    </MudCardContent>
</MudCard>
```

### 2. Snackbar für Feedback
```csharp
@inject ISnackbar Snackbar

Snackbar.Add("Erfolgreich gespeichert!", Severity.Success);
```

### 3. Dialoge für Bestätigungen
```csharp
@inject IDialogService DialogService

bool? result = await DialogService.ShowMessageBox(
    "Bestätigung",
    "Wirklich löschen?",
    yesText: "Ja", cancelText: "Nein"
);
```

### 4. Loading States
```razor
@if (isLoading)
{
    <MudProgressLinear Indeterminate="true" Color="Color.Primary" />
}
else
{
    <!-- Content -->
}
```

---

## 🎓 Lernressourcen

- [MudBlazor Dokumentation](https://mudblazor.com/getting-started/installation)
- [MudBlazor Komponenten](https://mudblazor.com/components/list)
- [Blazor Tutorial](https://learn.microsoft.com/aspnet/core/blazor/)
- [C# Guide](https://learn.microsoft.com/dotnet/csharp/)

---

## 📞 Support

Bei Fragen oder Problemen:
1. `SETUP-GUIDE.md` lesen
2. `IMPLEMENTATION-STATUS.md` für TODOs
3. MudBlazor Docs konsultieren
4. GitHub Issues durchsuchen

---

**Viel Erfolg mit der HTL Krems Notenverwaltung! 🚀**

*Erstellt am: 03.11.2025*  
*Blazor Version: .NET 8*  
*MudBlazor Version: 7.0*
