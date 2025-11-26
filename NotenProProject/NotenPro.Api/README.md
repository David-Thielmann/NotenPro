# HTL Krems - Notenverwaltungssoftware (MudBlazor)

Eine moderne, webbasierte Notenverwaltungssoftware für Lehrkräfte und Schulen, entwickelt mit Blazor WebAssembly und MudBlazor.

## 🎓 Features

### Vier rollenbasierte Benutzeroberflächen:

1. **Schüler-Ansicht**
   - Dashboard mit Notenübersicht
   - Detaillierte Notenliste mit Filterung
   - Benachrichtigungen
   - Raumfinder (Platzhalter)
   - Mobile Bottom-Navigation

2. **Lehrer-Ansicht**
   - Dashboard mit Statistiken
   - Test-Verwaltung
   - Noteneintragung
   - Frühwarnungen
   - Klassenübersicht

3. **Schuladministrator-Ansicht**
   - Dashboard mit Schulstatistiken
   - Lehrerverwaltung (CRUD)
   - Klassenverwaltung
   - Fächerverwaltung
   - Raumplan-Upload
   - Backup-Verwaltung
   - Audit-Log

4. **Systemverwalter-Ansicht**
   - System-Dashboard
   - Schulenverwaltung
   - Administrator-Verwaltung
   - System-Monitoring

## 🚀 Installation & Start

### Voraussetzungen
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Ein Code-Editor (z.B. Visual Studio, VS Code, Rider)

### Schritt 1: Projekt erstellen

```bash
# Neues Blazor WebAssembly Projekt erstellen
dotnet new blazorwasm -o HTLKrems.GradeManagement
cd HTLKrems.GradeManagement
```

### Schritt 2: MudBlazor installieren

```bash
dotnet add package MudBlazor
```

### Schritt 3: Dateien kopieren

Kopieren Sie alle Dateien aus dem `blazor-export` Ordner in Ihr Projekt:

```
HTLKrems.GradeManagement/
├── Program.cs                  (ersetzen)
├── App.razor                   (ersetzen)
├── _Imports.razor              (ersetzen)
├── Models/
│   └── Models.cs               (neu)
├── Services/
│   ├── AuthService.cs          (neu)
│   ├── AllServices.cs          (neu)
│   └── GradeService.cs         (neu)
├── Pages/
│   ├── Login.razor             (neu)
│   ├── Student/
│   │   ├── Dashboard.razor     (neu)
│   │   ├── Grades.razor        (neu)
│   │   └── Notifications.razor (neu)
│   ├── Teacher/
│   │   └── Dashboard.razor     (neu)
│   └── Admin/
│       ├── Dashboard.razor     (neu)
│       └── Teachers.razor      (neu)
├── Shared/
│   ├── MainLayout.razor        (ersetzen)
│   └── StudentLayout.razor     (neu)
└── wwwroot/
    ├── index.html              (ersetzen)
    └── css/
        └── app.css             (ersetzen)
```

### Schritt 4: Projekt starten

```bash
dotnet run
```

Die Anwendung ist nun unter `https://localhost:5001` oder `http://localhost:5000` erreichbar.

## 🔐 Demo-Zugänge

Die Anwendung enthält vier Demo-Benutzer zum Testen:

| Rolle              | E-Mail                     | Passwort  |
|--------------------|----------------------------|-----------|
| Schüler            | student@htl-krems.at       | student   |
| Lehrer             | teacher@htl-krems.at       | teacher   |
| Schuladministrator | admin@htl-krems.at         | admin     |
| Systemverwalter    | sysadmin@htl-krems.at      | sysadmin  |

## 📱 Responsive Design

Die Anwendung ist vollständig responsive:
- **Desktop**: Sidebar-Navigation mit allen Features
- **Tablet**: Kollabierbare Sidebar
- **Mobile**: 
  - Schüler: Bottom-Navigation mit 4 Tabs
  - Andere Rollen: Hamburger-Menü

## 🎨 Design-System

### Farben
- **Primärfarbe**: #29aae3 (HTL Krems Blau)
- **Hover**: #1a8cc2
- **Schriftart**: Inter

### Komponenten
Die Anwendung nutzt MudBlazor-Komponenten:
- `MudTable` für Tabellen
- `MudCard` für Karten
- `MudDialog` für Dialoge
- `MudButton` für Buttons
- `MudTextField` für Eingabefelder
- `MudSelect` für Dropdown-Menüs
- `MudSnackbar` für Toast-Benachrichtigungen

## 📁 Projekt-Struktur

```
HTLKrems.GradeManagement/
├── Models/              # Datenmodelle (User, Grade, Test, etc.)
├── Services/            # Business Logic & Mock-Daten
│   ├── AuthService         - Authentifizierung
│   ├── GradeService        - Notenverwaltung
│   ├── TestService         - Test-Verwaltung
│   ├── NotificationService - Benachrichtigungen
│   ├── TeacherService      - Lehrerverwaltung
│   ├── ClassService        - Klassenverwaltung
│   ├── SubjectService      - Fächerverwaltung
│   └── SchoolService       - Schulenverwaltung
├── Pages/               # Razor-Seiten (Views)
│   ├── Login.razor         - Login-Seite
│   ├── Student/            - Schüler-Seiten
│   ├── Teacher/            - Lehrer-Seiten
│   ├── Admin/              - Schuladmin-Seiten
│   └── SysAdmin/           - Systemadmin-Seiten
├── Shared/              # Layouts
│   ├── MainLayout.razor    - Haupt-Layout
│   └── StudentLayout.razor - Schüler-spezifisches Layout
└── wwwroot/             # Statische Dateien
    ├── index.html
    └── css/app.css
```

## 🔧 Nächste Schritte für Production

### 1. Backend API erstellen
Die aktuellen Services verwenden Mock-Daten. Für Production benötigen Sie:

```csharp
// ASP.NET Core Web API
builder.Services.AddScoped(sp => 
    new HttpClient { BaseAddress = new Uri("https://your-api.com") });
```

### 2. Datenbank einrichten
- Entity Framework Core für Datenpersistenz
- SQL Server / PostgreSQL / MySQL

### 3. Authentifizierung
- JWT Token-basierte Auth
- ASP.NET Core Identity
- Refresh Tokens

### 4. Weitere Features implementieren
- [ ] Test-Erstellung Dialog
- [ ] Noteneingabe-Formulare
- [ ] Frühwarnungs-Verwaltung
- [ ] Klassenübersicht für Lehrer
- [ ] Fächer-Verwaltung für Admin
- [ ] System-Monitoring für SysAdmin
- [ ] Export-Funktionen (PDF, CSV)
- [ ] Raumfinder mit Kartenintegration

### 5. Deployment
```bash
# Publish für Production
dotnet publish -c Release
```

Deploy nach:
- Azure Static Web Apps
- GitHub Pages
- Netlify
- Eigener Server (IIS, Apache, Nginx)

## 🛠️ Entwicklung

### Neue Seite hinzufügen

```razor
@page "/neue-seite"
@inject IService Service

<PageTitle>Neue Seite</PageTitle>

<MudText Typo="Typo.h4">Neue Seite</MudText>

@code {
    protected override async Task OnInitializedAsync()
    {
        // Initialisierungslogik
    }
}
```

### Neuen Service hinzufügen

```csharp
// 1. Interface definieren
public interface IMyService
{
    Task<List<MyModel>> GetDataAsync();
}

// 2. Service implementieren
public class MyService : IMyService
{
    public Task<List<MyModel>> GetDataAsync()
    {
        // Implementation
    }
}

// 3. In Program.cs registrieren
builder.Services.AddScoped<IMyService, MyService>();
```

## 📚 Ressourcen

- [MudBlazor Dokumentation](https://mudblazor.com/)
- [Blazor Dokumentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [.NET Documentation](https://learn.microsoft.com/en-us/dotnet/)

## 🎯 Status

✅ **Implementiert:**
- Login-System mit 4 Rollen
- Schüler-Dashboard
- Schüler-Notenübersicht
- Schüler-Benachrichtigungen
- Lehrer-Dashboard
- Schuladmin-Dashboard
- Schuladmin-Lehrerverwaltung
- Responsive Navigation
- Mobile Bottom-Navigation (Schüler)
- MudBlazor Theme (HTL Krems)

🚧 **In Arbeit:**
- Weitere Teacher-Seiten (Tests, Noten, Warnings)
- Weitere Admin-Seiten (Classes, Subjects, Maps, Backups)
- SysAdmin-Seiten
- Backend API Integration

## 📄 Lizenz

Dieses Projekt ist ein Prototyp für die HTL Krems Notenverwaltung.

## 👥 Kontakt

Für Fragen oder Feedback zur Anwendung, wenden Sie sich an das Entwicklerteam.

---

**Hinweis**: Diese Anwendung verwendet derzeit Mock-Daten und ist noch nicht für den Produktiveinsatz geeignet. Für Production muss eine Backend-API mit Datenbank implementiert werden.
