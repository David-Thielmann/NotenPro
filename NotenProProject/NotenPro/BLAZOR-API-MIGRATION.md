# 🔄 Blazor Client → API Migration Guide

## Von Mock-Daten zur echten API

Dein Blazor WebAssembly Client verwendet aktuell Mock-Daten. Diese Anleitung zeigt, wie du ihn auf die neue NotenPro API umstellst.

---

## 📋 Übersicht

### Aktueller Stand (Blazor)
- ✅ Mock-Services mit In-Memory Daten
- ✅ Alle UI-Komponenten funktionsfähig
- ✅ Login-System (ohne echte Validierung)
- ✅ Alle 4 Rollen-Dashboards

### Neuer Stand (mit API)
- ✅ Echte Datenbank (MySQL)
- ✅ RESTful API mit Entity Framework
- ✅ Persistente Daten
- ✅ Multi-User Support

---

## 🔧 Migration Steps

### 1. API starten

```bash
cd NotenPro.Api
dotnet run
```

API läuft auf: `http://localhost:5000`

### 2. HttpClient konfigurieren

**Datei:** `Program.cs`

**Aktuell:**
```csharp
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
});
```

**Ändern zu:**
```csharp
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("http://localhost:5000/") 
});
```

### 3. Services umstellen

Für jeden Service (AuthService, GradeService, etc.):

#### Beispiel: AuthService

**Aktuell (Mock):**
```csharp
public class AuthService : IAuthService
{
    private User? _currentUser;
    
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        await Task.Delay(500); // Simulate API call
        
        // Mock user lookup
        if (request.Email == "admin@example.com")
        {
            _currentUser = new User { ... };
            return new LoginResponse { Success = true, User = _currentUser };
        }
        
        return new LoginResponse { Success = false, ErrorMessage = "Invalid credentials" };
    }
}
```

**Neu (mit API):**
```csharp
public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private User? _currentUser;
    private string? _token;
    
    public AuthService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", request);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            
            if (result?.Success == true && result.User != null)
            {
                _currentUser = result.User;
                _token = result.Token;
                
                // Optional: Token in LocalStorage speichern
                // await _localStorage.SetItemAsync("authToken", _token);
            }
            
            return result ?? new LoginResponse { Success = false };
        }
        catch (Exception ex)
        {
            return new LoginResponse 
            { 
                Success = false, 
                ErrorMessage = $"Error: {ex.Message}" 
            };
        }
    }
    
    public async Task<User?> GetCurrentUserAsync()
    {
        if (_currentUser != null)
            return _currentUser;
            
        // Optional: Token aus LocalStorage laden und verifizieren
        // var token = await _localStorage.GetItemAsync<string>("authToken");
        // if (!string.IsNullOrEmpty(token))
        // {
        //     _http.DefaultRequestHeaders.Authorization = 
        //         new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //     
        //     var response = await _http.GetAsync("api/auth/verify");
        //     if (response.IsSuccessStatusCode)
        //     {
        //         var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        //         _currentUser = result?.User;
        //     }
        // }
        
        return _currentUser;
    }
    
    public async Task LogoutAsync()
    {
        _currentUser = null;
        _token = null;
        _http.DefaultRequestHeaders.Authorization = null;
        
        // Optional: Token aus LocalStorage entfernen
        // await _localStorage.RemoveItemAsync("authToken");
    }
}
```

#### Beispiel: GradeService

**Aktuell (Mock):**
```csharp
public async Task<List<Grade>> GetStudentGradesAsync(string studentId)
{
    await Task.Delay(300);
    return _mockGrades.Where(g => g.StudentId == studentId).ToList();
}
```

**Neu (mit API):**
```csharp
public async Task<List<Grade>> GetStudentGradesAsync(string studentId)
{
    try
    {
        var grades = await _http.GetFromJsonAsync<List<Grade>>($"api/grades/student/{studentId}");
        return grades ?? new List<Grade>();
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error loading grades: {ex.Message}");
        return new List<Grade>();
    }
}

public async Task<bool> CreateGradeAsync(Grade grade)
{
    try
    {
        var response = await _http.PostAsJsonAsync("api/grades", grade);
        return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error creating grade: {ex.Message}");
        return false;
    }
}

public async Task<bool> UpdateGradeAsync(string id, Grade grade)
{
    try
    {
        var response = await _http.PutAsJsonAsync($"api/grades/{id}", grade);
        return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error updating grade: {ex.Message}");
        return false;
    }
}

public async Task<bool> DeleteGradeAsync(string id)
{
    try
    {
        var response = await _http.DeleteAsync($"api/grades/{id}");
        return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error deleting grade: {ex.Message}");
        return false;
    }
}
```

---

## 📝 Service Migration Checklist

### AuthService
- [x] `LoginAsync()` → `POST /api/auth/login`
- [x] `RegisterAsync()` → `POST /api/auth/register`
- [x] `GetCurrentUserAsync()` → `GET /api/auth/verify`
- [x] `LogoutAsync()` → LocalStorage clear

### GradeService
- [ ] `GetAllGradesAsync()` → `GET /api/grades`
- [ ] `GetStudentGradesAsync(studentId)` → `GET /api/grades/student/{id}`
- [ ] `GetTestGradesAsync(testId)` → `GET /api/grades/test/{id}`
- [ ] `CreateGradeAsync(grade)` → `POST /api/grades`
- [ ] `UpdateGradeAsync(id, grade)` → `PUT /api/grades/{id}`
- [ ] `DeleteGradeAsync(id)` → `DELETE /api/grades/{id}`

### TestService
- [ ] `GetAllTestsAsync()` → `GET /api/tests`
- [ ] `GetTeacherTestsAsync(teacherId)` → `GET /api/tests/teacher/{id}`
- [ ] `GetClassTestsAsync(classId)` → `GET /api/tests/class/{id}`
- [ ] `CreateTestAsync(test)` → `POST /api/tests?teacherId={id}`
- [ ] `UpdateTestAsync(id, test)` → `PUT /api/tests/{id}`
- [ ] `DeleteTestAsync(id)` → `DELETE /api/tests/{id}`

### NotificationService
- [ ] `GetUserNotificationsAsync(userId)` → `GET /api/notifications/user/{id}`
- [ ] `GetUnreadNotificationsAsync(userId)` → `GET /api/notifications/user/{id}/unread`
- [ ] `GetUnreadCountAsync(userId)` → `GET /api/notifications/user/{id}/count`
- [ ] `MarkAsReadAsync(id)` → `PUT /api/notifications/{id}/read`
- [ ] `MarkAllAsReadAsync(userId)` → `POST /api/notifications/user/{id}/mark-all-read`
- [ ] `CreateNotificationAsync(notification)` → `POST /api/notifications`

### ClassService
- [ ] `GetAllClassesAsync(schoolId)` → `GET /api/classes?schoolId={id}`
- [ ] `GetClassAsync(id)` → `GET /api/classes/{id}`
- [ ] `GetClassStudentsAsync(id)` → `GET /api/classes/{id}/students`
- [ ] `CreateClassAsync(class)` → `POST /api/classes`
- [ ] `UpdateClassAsync(id, class)` → `PUT /api/classes/{id}`
- [ ] `DeleteClassAsync(id)` → `DELETE /api/classes/{id}`
- [ ] `AddStudentToClassAsync(classId, studentId)` → `POST /api/classes/{classId}/students/{studentId}`

### SubjectService
- [ ] `GetAllSubjectsAsync(schoolId)` → `GET /api/subjects?schoolId={id}`
- [ ] `GetTeacherSubjectsAsync(teacherId)` → `GET /api/subjects/teacher/{id}`
- [ ] `CreateSubjectAsync(subject)` → `POST /api/subjects`
- [ ] `UpdateSubjectAsync(id, subject)` → `PUT /api/subjects/{id}`
- [ ] `DeleteSubjectAsync(id)` → `DELETE /api/subjects/{id}`

### SchoolService
- [ ] `GetAllSchoolsAsync()` → `GET /api/schools`
- [ ] `GetSchoolAsync(id)` → `GET /api/schools/{id}`
- [ ] `CreateSchoolAsync(school)` → `POST /api/schools`
- [ ] `UpdateSchoolAsync(id, school)` → `PUT /api/schools/{id}`
- [ ] `DeleteSchoolAsync(id)` → `DELETE /api/schools/{id}`
- [ ] `GetSchoolStatisticsAsync(id)` → `GET /api/schools/{id}/statistics`

### StudentService
- [ ] `GetAllStudentsAsync(schoolId, classId)` → `GET /api/users?role=Student&schoolId={id}&classId={id}`
- [ ] `GetStudentAsync(id)` → `GET /api/users/{id}`

### TeacherService
- [ ] `GetAllTeachersAsync(schoolId)` → `GET /api/users?role=Teacher&schoolId={id}`
- [ ] `GetTeacherAsync(id)` → `GET /api/users/{id}`

---

## 🔐 Authentication mit Token

### Option 1: LocalStorage (einfach)

```csharp
// Install Package: Blazored.LocalStorage
// dotnet add package Blazored.LocalStorage

// Program.cs
builder.Services.AddBlazoredLocalStorage();

// AuthService.cs
private readonly ILocalStorageService _localStorage;

public AuthService(HttpClient http, ILocalStorageService localStorage)
{
    _http = http;
    _localStorage = localStorage;
}

public async Task<LoginResponse> LoginAsync(LoginRequest request)
{
    var response = await _http.PostAsJsonAsync("api/auth/login", request);
    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
    
    if (result?.Success == true && result.Token != null)
    {
        await _localStorage.SetItemAsync("authToken", result.Token);
        _http.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", result.Token);
    }
    
    return result;
}

public async Task InitializeAsync()
{
    var token = await _localStorage.GetItemAsync<string>("authToken");
    if (!string.IsNullOrEmpty(token))
    {
        _http.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        // Optional: Token verifizieren
    }
}
```

### Option 2: AuthenticationStateProvider (empfohlen)

```csharp
public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public CustomAuthStateProvider(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrEmpty(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        _http.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);

        // Token verifizieren und User-Daten laden
        try
        {
            var response = await _http.GetAsync("api/auth/verify");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (result?.Success == true && result.User != null)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, result.User.Name),
                        new Claim(ClaimTypes.Email, result.User.Email),
                        new Claim(ClaimTypes.Role, result.User.Role),
                        new Claim(ClaimTypes.NameIdentifier, result.User.Id)
                    };
                    
                    var identity = new ClaimsIdentity(claims, "apiauth");
                    var user = new ClaimsPrincipal(identity);
                    
                    return new AuthenticationState(user);
                }
            }
        }
        catch { }

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public async Task MarkUserAsAuthenticated(LoginResponse loginResponse)
    {
        await _localStorage.SetItemAsync("authToken", loginResponse.Token);
        _http.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", loginResponse.Token);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, loginResponse.User.Name),
            new Claim(ClaimTypes.Email, loginResponse.User.Email),
            new Claim(ClaimTypes.Role, loginResponse.User.Role),
            new Claim(ClaimTypes.NameIdentifier, loginResponse.User.Id)
        };
        
        var identity = new ClaimsIdentity(claims, "apiauth");
        var user = new ClaimsPrincipal(identity);
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _http.DefaultRequestHeaders.Authorization = null;

        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
}

// Program.cs
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
```

---

## 🐛 Troubleshooting

### CORS Fehler

**Problem:** Browser blockiert API-Calls

**Lösung:** API läuft bereits mit CORS-Support. Falls Probleme:

`NotenPro.Api/Program.cs`:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// ...

app.UseCors("AllowBlazorClient");
```

### Connection Refused

**Problem:** `HttpRequestException: Connection refused`

**Lösung:** 
1. API läuft auf Port 5000? `dotnet run` in NotenPro.Api
2. BaseAddress korrekt? `http://localhost:5000/`
3. Firewall blockiert?

### 401 Unauthorized

**Problem:** API gibt 401 zurück

**Lösung:** 
- Aktuell: API hat noch keine Authorization → sollte nicht passieren
- Später mit JWT: Token im Header senden

### Leere Responses

**Problem:** API gibt leere Arrays zurück

**Lösung:**
- Datenbank leer? Seed-Daten beim ersten Start angelegt?
- Falsche IDs verwendet?
- Console Logs checken

---

## 📈 Schrittweise Migration (Empfohlen)

### Phase 1: Auth & Users
1. AuthService umstellen
2. Login-Page testen
3. UserService umstellen
4. User-Verwaltung testen

### Phase 2: Core Entities
1. SchoolService umstellen
2. ClassService umstellen
3. SubjectService umstellen

### Phase 3: Academic
1. TestService umstellen
2. GradeService umstellen
3. Noten-Eingabe testen

### Phase 4: Notifications
1. NotificationService umstellen
2. Benachrichtigungs-System testen

### Phase 5: Polish
1. Error Handling verbessern
2. Loading States hinzufügen
3. Offline-Support (optional)

---

## ✅ Test-Checklist

Nach Migration testen:

- [ ] Login funktioniert
- [ ] Logout funktioniert
- [ ] Schüler-Dashboard zeigt echte Noten
- [ ] Lehrer kann Tests erstellen
- [ ] Lehrer kann Noten eintragen
- [ ] Benachrichtigungen werden angezeigt
- [ ] Admin kann Klassen verwalten
- [ ] Admin kann Benutzer verwalten
- [ ] System-Admin kann Schulen verwalten
- [ ] Daten bleiben nach Reload erhalten

---

## 🚀 Production Checklist

Für Production-Deployment:

- [ ] Echte JWT-Tokens implementieren
- [ ] HTTPS erzwingen
- [ ] Environment-basierte API-URL
- [ ] Error Logging (Sentry, AppInsights)
- [ ] Performance Monitoring
- [ ] Rate Limiting
- [ ] Input Validation verschärfen
- [ ] Security Headers
- [ ] Backup-System

---

**Viel Erfolg bei der Migration! 🎉**

Bei Fragen: Siehe API-EXAMPLES.md für Request/Response Beispiele
