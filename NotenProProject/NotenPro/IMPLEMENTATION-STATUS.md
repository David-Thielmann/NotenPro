# Implementierungs-Status

## ✅ Vollständig implementiert

### Core-Funktionalität
- [x] Project Setup (Program.cs, .csproj, _Imports.razor)
- [x] MudBlazor Theme Integration (HTL Krems Design)
- [x] Responsive Layout (Desktop, Tablet, Mobile)
- [x] Login-System mit 4 Rollen
- [x] Routing & Navigation
- [x] MainLayout mit Sidebar
- [x] StudentLayout mit Mobile Bottom-Navigation
- [x] Mock-Services mit Demo-Daten

### Datenmodelle
- [x] User, UserRole Enum
- [x] Grade, GradeStatus Enum
- [x] Test, TestType Enum
- [x] Notification, NotificationType Enum
- [x] Teacher, Class, Subject
- [x] School
- [x] DTOs (LoginRequest, LoginResponse, CreateTestRequest, ApiResponse)

### Services
- [x] AuthService (Login, Logout, GetCurrentUser)
- [x] GradeService (GetMyGrades, GetRecentGrades, GetSubjectAverages)
- [x] NotificationService (GetNotifications, GetUnreadCount, MarkAsRead)
- [x] StudentService (GetDashboardStats)
- [x] TestService (GetMyTests, CreateTest, DeleteTest)
- [x] TeacherService (GetTeachers, CreateTeacher, DeleteTeacher)
- [x] ClassService (GetClasses, CreateClass)
- [x] SubjectService (GetSubjects, CreateSubject, DeleteSubject)
- [x] SchoolService (GetSchools, CreateSchool)

### Seiten - Schüler
- [x] Login.razor
- [x] Student/Dashboard.razor
- [x] Student/Grades.razor
- [x] Student/Notifications.razor
- [ ] Student/Map.razor (TODO)

### Seiten - Lehrer
- [x] Teacher/Dashboard.razor
- [ ] Teacher/Tests.razor (TODO)
- [ ] Teacher/TestCreate.razor (TODO)
- [ ] Teacher/GradeEntry.razor (TODO)
- [ ] Teacher/Warnings.razor (TODO)
- [ ] Teacher/Classes.razor (TODO)

### Seiten - Schuladministrator
- [x] Admin/Dashboard.razor
- [x] Admin/Teachers.razor
- [ ] Admin/Classes.razor (TODO)
- [ ] Admin/Subjects.razor (TODO)
- [ ] Admin/Maps.razor (TODO)
- [ ] Admin/Backups.razor (TODO)
- [ ] Admin/Audit.razor (TODO)

### Seiten - Systemverwalter
- [ ] SysAdmin/Dashboard.razor (TODO)
- [ ] SysAdmin/Schools.razor (TODO)
- [ ] SysAdmin/Admins.razor (TODO)
- [ ] SysAdmin/Monitoring.razor (TODO)
- [ ] SysAdmin/Settings.razor (TODO)

---

## 🚧 Zu implementieren

### Priorität HOCH

#### Teacher/Tests.razor
```razor
@page "/teacher/tests"
- Tabelle aller Tests
- Filter nach Klasse, Fach, Datum
- "Neuer Test" Button → Dialog
- Bearbeiten/Löschen Buttons
- Noten eintragen Button
```

#### Teacher/GradeEntry.razor
```razor
@page "/teacher/grades/{TestId}"
- Test-Info anzeigen
- Tabelle mit allen Schülern
- Eingabefelder: Note, Punkte, Status, Kommentar
- Speichern-Button (einzeln oder alle)
- Validierung
```

#### Admin/Classes.razor
```razor
@page "/admin/classes"
- Tabelle aller Klassen
- CRUD-Operationen
- Filter und Suche
- Klassenvorstand zuweisen
```

#### Admin/Subjects.razor
```razor
@page "/admin/subjects"
- Tabelle/Card-Liste aller Fächer
- CRUD-Operationen
- Aktiv/Inaktiv Toggle
```

### Priorität MITTEL

#### Teacher/Warnings.razor
```razor
@page "/teacher/warnings"
- Liste Schüler mit Durchschnitt < 3.5
- Dialog: Frühwarnung erstellen
- Gesendete Frühwarnungen anzeigen
```

#### Teacher/Classes.razor
```razor
@page "/teacher/classes"
- Meine Klassen
- Schülerliste pro Klasse
- Notenübersicht pro Klasse
- Statistiken
```

#### Admin/Maps.razor
```razor
@page "/admin/maps"
- Datei-Upload (Drag & Drop)
- Stockwerk auswählen
- Liste hochgeladener Pläne
- Vorschau, Download, Löschen
```

#### Admin/Backups.razor
```razor
@page "/admin/backups"
- "Backup erstellen" Button
- Liste aller Backups (Datum, Größe, Typ)
- Download-Button
- Automatische Backup-Einstellungen
```

#### Admin/Audit.razor
```razor
@page "/admin/audit"
- Tabelle: User, Action, Timestamp, Details
- Filter nach User/Action/Datum
- Export-Funktion
```

### Priorität NIEDRIG

#### SysAdmin/Dashboard.razor
```razor
@page "/sysadmin"
- System-Statistiken (Schulen, User, Schüler)
- Aktivitäts-Feed
- System-Status
```

#### SysAdmin/Schools.razor
```razor
@page "/sysadmin/schools"
- Tabelle aller Schulen
- CRUD-Operationen
- Status ändern (Aktiv/Inaktiv/Gesperrt)
- Administrator zuweisen
```

#### SysAdmin/Admins.razor
```razor
@page "/sysadmin/admins"
- Tabelle aller Schuladministratoren
- CRUD-Operationen
- Schule zuordnen
```

#### Student/Map.razor
```razor
@page "/student/map"
- Interaktive Karte anzeigen
- Zoom & Pan
- Stockwerk-Auswahl
- Raumsuche
```

---

## 🎨 UI-Verbesserungen (Optional)

- [ ] Loading Skeletons statt einfacher Progress Bar
- [ ] Animations (MudBlazor Transitions)
- [ ] Charts mit MudBlazor Charts
- [ ] Dark Mode Toggle
- [ ] Excel/PDF Export Funktionen
- [ ] Print-Stylesheets
- [ ] Offline-Support (Service Worker)
- [ ] Bildoptimierung & Lazy Loading

---

## 🔧 Backend-Integration

### API Endpoints benötigt

```
Authentication:
POST   /api/auth/login
POST   /api/auth/logout
GET    /api/auth/me

Grades:
GET    /api/grades/me
GET    /api/grades/recent?count=5
GET    /api/grades/subject-averages
POST   /api/grades/bulk

Tests:
GET    /api/tests
GET    /api/tests/{id}
POST   /api/tests
PUT    /api/tests/{id}
DELETE /api/tests/{id}

Notifications:
GET    /api/notifications
GET    /api/notifications/unread-count
PUT    /api/notifications/{id}/mark-read

Teachers:
GET    /api/teachers
POST   /api/teachers
PUT    /api/teachers/{id}
DELETE /api/teachers/{id}

Classes:
GET    /api/classes
POST   /api/classes
PUT    /api/classes/{id}
DELETE /api/classes/{id}

Subjects:
GET    /api/subjects
POST   /api/subjects
PUT    /api/subjects/{id}
DELETE /api/subjects/{id}

Schools:
GET    /api/schools
POST   /api/schools
PUT    /api/schools/{id}
DELETE /api/schools/{id}
```

### Datenbank Schema

```sql
-- Users Table
CREATE TABLE Users (
    Id VARCHAR(36) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role INT NOT NULL,
    SchoolId VARCHAR(36),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Grades Table
CREATE TABLE Grades (
    Id VARCHAR(36) PRIMARY KEY,
    StudentId VARCHAR(36) NOT NULL,
    TestId VARCHAR(36) NOT NULL,
    GradeValue DECIMAL(3,1) NOT NULL,
    Points INT,
    MaxPoints INT,
    Status INT DEFAULT 0,
    Comment VARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StudentId) REFERENCES Users(Id),
    FOREIGN KEY (TestId) REFERENCES Tests(Id)
);

-- Tests Table
CREATE TABLE Tests (
    Id VARCHAR(36) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    SubjectId VARCHAR(36) NOT NULL,
    ClassId VARCHAR(36) NOT NULL,
    TeacherId VARCHAR(36) NOT NULL,
    Date DATE NOT NULL,
    MaxPoints INT NOT NULL,
    Type INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (SubjectId) REFERENCES Subjects(Id),
    FOREIGN KEY (ClassId) REFERENCES Classes(Id),
    FOREIGN KEY (TeacherId) REFERENCES Users(Id)
);

-- Weitere Tabellen: Notifications, Teachers, Classes, Subjects, Schools, etc.
```

---

## 📋 Checkliste für Production

### Sicherheit
- [ ] JWT Token-basierte Authentifizierung
- [ ] HTTPS erzwingen
- [ ] CSRF Protection
- [ ] Input Validierung & Sanitization
- [ ] SQL Injection Prevention
- [ ] XSS Prevention
- [ ] Rate Limiting
- [ ] Password Hashing (bcrypt)

### Performance
- [ ] Lazy Loading für große Listen
- [ ] Virtualization bei langen Tabellen
- [ ] Caching-Strategien
- [ ] CDN für statische Assets
- [ ] Code Splitting
- [ ] Image Optimization
- [ ] Debouncing bei Suchen

### Qualität
- [ ] Unit Tests
- [ ] Integration Tests
- [ ] E2E Tests
- [ ] Error Logging (Sentry, Application Insights)
- [ ] Performance Monitoring
- [ ] User Analytics

### DevOps
- [ ] CI/CD Pipeline
- [ ] Automated Deployment
- [ ] Environment Variables
- [ ] Database Migrations
- [ ] Backup Strategy
- [ ] Monitoring & Alerts

---

## 💡 Schnellstart für Entwickler

### Neue Seite erstellen
1. Razor-Datei in `/Pages/[Rolle]/` erstellen
2. Service-Methoden falls nötig hinzufügen
3. Navigation in MainLayout.razor eintragen

### Beispiel: Teacher/Tests.razor

```razor
@page "/teacher/tests"
@inject ITestService TestService
@inject ISnackbar Snackbar

<PageTitle>Tests</PageTitle>

<MudText Typo="Typo.h4" Class="mb-6">Meine Tests</MudText>

<MudTable Items="@tests" Hover="true">
    <HeaderContent>
        <MudTh>Name</MudTh>
        <MudTh>Fach</MudTh>
        <MudTh>Datum</MudTh>
        <MudTh>Aktionen</MudTh>
    </HeaderContent>
    <RowTemplate>
        <MudTd>@context.Name</MudTd>
        <MudTd>@context.Subject</MudTd>
        <MudTd>@context.Date.ToString("dd.MM.yyyy")</MudTd>
        <MudTd>
            <MudIconButton Icon="@Icons.Material.Filled.Edit" Size="Size.Small" />
            <MudIconButton Icon="@Icons.Material.Filled.Delete" Size="Size.Small" Color="Color.Error" />
        </MudTd>
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

---

**Status**: Ca. 30% der Funktionalität implementiert. Grundgerüst steht, alle wichtigen Services vorhanden, weitere Seiten können schnell hinzugefügt werden.
