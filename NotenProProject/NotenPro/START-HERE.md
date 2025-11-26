# 🚀 NotenPro - START HIER!

## HTL Krems Notenverwaltungssystem

Willkommen! Diese Datei ist dein **Einstiegspunkt** ins Projekt.

---

## 📦 Was hast du?

Eine **vollständige .NET 9 Backend-API** mit MySQL-Datenbank:

✅ **8 REST-Controller** (70+ Endpoints)
✅ **10 Entity Models** mit Relations  
✅ **Entity Framework Core** mit Auto-Migrations  
✅ **Seed-Daten** (4 Demo-Accounts)  
✅ **Swagger UI** (interaktive API-Doku)  
✅ **Blazor WebAssembly Client** (Basis, 30%)  

---

## ⚡ Quick Start (5 Minuten)

### 1. MySQL vorbereiten

```bash
mysql -u root -p
```

```sql
CREATE DATABASE notenpro_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
EXIT;
```

### 2. Connection String setzen

**Datei:** `NotenPro.Api/appsettings.Development.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=notenpro_db;User=root;Password=DEIN_PASSWORT;"
  }
}
```

⚠️ **WICHTIG:** Ersetze `DEIN_PASSWORT` mit deinem MySQL root-Passwort!

### 3. API starten

```bash
cd NotenPro.Api
dotnet restore
dotnet run
```

### 4. Testen

Browser öffnen: **http://localhost:5000**

Du siehst die **Swagger UI**! ✅

### 5. Login testen

In Swagger:
1. `POST /api/auth/login` öffnen
2. "Try it out" klicken
3. Eingeben:
```json
{
  "email": "sysadmin@notenpro.at",
  "password": "Admin@123"
}
```
4. "Execute" klicken

✅ **Funktioniert?** Super! Die API läuft!

---

## 👥 Demo-Accounts

| Email | Passwort | Rolle |
|-------|----------|-------|
| sysadmin@notenpro.at | Admin@123 | System Admin |
| admin@htl-krems.ac.at | Admin@123 | School Admin |
| maria.schmidt@htl-krems.ac.at | Teacher@123 | Lehrer |
| max.mustermann@students.htl-krems.ac.at | Student@123 | Schüler |

---

## 📚 Dokumentation

| Datei | Was drin steht | Für wen |
|-------|---------------|---------|
| **QUICKSTART.md** | 5-Min Schnellstart | Alle |
| **README.md** | Vollständige API-Doku | Entwickler |
| **SETUP.md** | Detaillierte Anleitung | Neue Entwickler |
| **API-EXAMPLES.md** | Request-Beispiele | API-User |
| **BLAZOR-API-MIGRATION.md** | Blazor → API Migration | Frontend-Dev |
| **API-OVERVIEW.md** | Projekt-Übersicht | Project Manager |
| **PROJECT-STATUS.md** | Status & Roadmap | Alle |

**Empfehlung:** 
- **Neu hier?** → Lies **QUICKSTART.md**
- **API verwenden?** → Lies **README.md**
- **Frontend integrieren?** → Lies **BLAZOR-API-MIGRATION.md**

---

## 🗂️ Projektstruktur

```
NotenPro/
│
├── NotenPro.Api/                    ⭐ BACKEND API
│   ├── Controllers/                 # 8 REST-Controller
│   ├── Data/Entities/               # 10 Entity Models
│   ├── DTOs/                        # Data Transfer Objects
│   ├── Program.cs                   # API Startup
│   ├── appsettings.json             # Config
│   │
│   ├── README.md                    # 📖 API-Doku
│   ├── SETUP.md                     # 🔧 Setup-Guide
│   ├── QUICKSTART.md                # 🚀 Schnellstart
│   └── API-EXAMPLES.md              # 📝 Beispiele
│
├── Models/                          # Blazor Models
├── Services/                        # Blazor Services (Mock)
├── Pages/                           # Blazor Pages
│   ├── Student/                     # Schüler-Bereich
│   ├── Teacher/                     # Lehrer-Bereich
│   └── Admin/                       # Admin-Bereich
│
├── API-OVERVIEW.md                  # 📊 Projekt-Übersicht
├── BLAZOR-API-MIGRATION.md          # 🔄 Migration-Guide
├── PROJECT-STATUS.md                # 📈 Status & Roadmap
└── START-HERE.md                    # 👈 DU BIST HIER
```

---

## 🎯 Was als nächstes?

### Option 1: API erkunden (empfohlen für Neue)

1. **Swagger UI öffnen:** http://localhost:5000
2. **Alle Endpoints testen** (siehe API-EXAMPLES.md)
3. **Datenbank anschauen** (MySQL Workbench)

### Option 2: Blazor Client starten

```bash
cd HTLKrems.GradeManagement
dotnet run
```

**Hinweis:** Aktuell mit Mock-Daten! Siehe BLAZOR-API-MIGRATION.md für Integration.

### Option 3: Mit Development beginnen

1. **API-Integration:** Siehe BLAZOR-API-MIGRATION.md
2. **Neue Features:** Siehe PROJECT-STATUS.md → Roadmap
3. **Code-Review:** Erkunde die Controller

---

## 🔍 API Endpoints (Auswahl)

### Authentication
```
POST /api/auth/login          # Login
POST /api/auth/register       # Registrieren
GET  /api/auth/verify         # Token verifizieren
```

### Users
```
GET    /api/users                    # Alle Benutzer
GET    /api/users/{id}               # Benutzer-Details
GET    /api/users/students           # Alle Schüler
GET    /api/users/teachers           # Alle Lehrer
POST   /api/users                    # Neuer Benutzer
PUT    /api/users/{id}               # Benutzer updaten
DELETE /api/users/{id}               # Benutzer löschen
GET    /api/users/{id}/statistics    # Statistiken
```

### Grades (Noten)
```
GET  /api/grades/student/{id}   # Noten eines Schülers
GET  /api/grades/test/{id}      # Noten eines Tests
POST /api/grades                # Neue Note
POST /api/grades/bulk           # Bulk-Noten (ganze Klasse)
PUT  /api/grades/{id}           # Note updaten
```

### Tests
```
GET    /api/tests/teacher/{id}  # Tests eines Lehrers
GET    /api/tests/class/{id}    # Tests einer Klasse
POST   /api/tests               # Neuer Test (+ Auto-Noten)
PUT    /api/tests/{id}          # Test updaten
DELETE /api/tests/{id}          # Test löschen
```

### Notifications
```
GET  /api/notifications/user/{id}         # Benachrichtigungen
GET  /api/notifications/user/{id}/unread  # Ungelesene
POST /api/notifications/broadcast         # An mehrere senden
POST /api/notifications/mark-read         # Als gelesen
```

**TOTAL: 70+ Endpoints**

Alle Endpoints in **Swagger UI:** http://localhost:5000

---

## 💡 Tipps & Tricks

### IDs herausfinden

```bash
# Alle Schulen
curl http://localhost:5000/api/schools

# Alle Klassen einer Schule
curl "http://localhost:5000/api/classes?schoolId={SCHOOL_ID}"

# Alle Tests eines Lehrers
curl http://localhost:5000/api/tests/teacher/{TEACHER_ID}
```

### Workflow: Neuer Test mit Noten

```bash
# 1. Test erstellen (erstellt automatisch Pending-Noten für alle Schüler)
curl -X POST "http://localhost:5000/api/tests?teacherId={T_ID}" \
  -H "Content-Type: application/json" \
  -d '{"name":"Mathe Test 1","subjectId":"{S_ID}","classId":"{C_ID}",...}'

# 2. Noten für alle Schüler eintragen (Bulk)
curl -X POST http://localhost:5000/api/grades/bulk \
  -H "Content-Type: application/json" \
  -d '{"testId":"{TEST_ID}","grades":[...]}'

# 3. Schüler erhalten automatisch Benachrichtigungen!
```

### PowerShell statt cURL

```powershell
# Login
$body = @{ email = "sysadmin@notenpro.at"; password = "Admin@123" } | ConvertTo-Json
$result = Invoke-RestMethod -Uri "http://localhost:5000/api/auth/login" -Method Post -Body $body -ContentType "application/json"
$result

# GET Request
Invoke-RestMethod -Uri "http://localhost:5000/api/schools"
```

---

## 🐛 Troubleshooting

### MySQL Connection Error?
```bash
# MySQL läuft?
net start MySQL80  # Windows
sudo systemctl start mysql  # Linux

# Connection String richtig?
# Passwort korrekt?
```

### Port 5000 schon belegt?
Ändere in `NotenPro.Api/Properties/launchSettings.json`:
```json
"applicationUrl": "http://localhost:5555"
```

### Build-Fehler?
```bash
cd NotenPro.Api
dotnet clean
dotnet restore
dotnet build
```

### Datenbank zurücksetzen?
```bash
cd NotenPro.Api
dotnet ef database drop --force
dotnet run  # Erstellt DB neu mit Seed-Daten
```

---

## 📊 Projekt-Status

### ✅ Was funktioniert (100%)
- Backend API mit allen Controllern
- MySQL Datenbank mit Seed-Daten
- Swagger-Dokumentation
- CRUD für alle Entitäten
- Bulk-Operations
- Auto-Notifications
- Statistiken

### 🔄 Work in Progress (30%)
- Blazor Client (Basis-UI vorhanden)
- Mock-Services → API-Integration

### ⏳ Noch zu tun
- JWT Authentication (statt Mock-Tokens)
- Alle Blazor-Seiten vervollständigen
- Email-Benachrichtigungen
- Reports & Excel-Export
- Production Deployment

**Details:** Siehe PROJECT-STATUS.md

---

## 🎓 Learning Resources

### .NET 9 & Entity Framework
- [Microsoft Docs - EF Core](https://learn.microsoft.com/ef/core/)
- [ASP.NET Core Tutorial](https://learn.microsoft.com/aspnet/core/tutorials/first-web-api)

### Blazor WebAssembly
- [Blazor Docs](https://learn.microsoft.com/aspnet/core/blazor/)
- [MudBlazor Components](https://mudblazor.com/)

### MySQL
- [MySQL Documentation](https://dev.mysql.com/doc/)
- [Pomelo EF Core Provider](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)

---

## 🤝 Contributing

### Code Standards
- `.editorconfig` für Coding Style
- C# Naming Conventions
- RESTful API Best Practices

### Workflow
1. Feature Branch erstellen
2. Code schreiben
3. Testen
4. Pull Request
5. Code Review
6. Merge

---

## 📞 Support

### Bei Problemen:

1. **QUICKSTART.md** lesen
2. **SETUP.md** für Details
3. **Swagger UI** für API-Tests: http://localhost:5000
4. **MySQL Workbench** für Datenbank-Check
5. **Console Logs** prüfen

### Häufige Fragen → **SETUP.md** → "Troubleshooting"

---

## ✅ Checkliste

Hake ab, wenn du fertig bist:

- [ ] MySQL installiert & läuft
- [ ] Datenbank `notenpro_db` erstellt
- [ ] Connection String in appsettings.Development.json gesetzt
- [ ] `dotnet restore` ausgeführt
- [ ] `dotnet run` startet ohne Fehler
- [ ] Swagger UI erreichbar (http://localhost:5000)
- [ ] Login getestet (sysadmin@notenpro.at / Admin@123)
- [ ] Ein paar Endpoints in Swagger ausprobiert
- [ ] Dokumentation gelesen (README.md)
- [ ] Bereit für Development! 🎉

---

## 🎉 Fertig!

Du bist jetzt bereit, mit **NotenPro** zu arbeiten!

### Nächste Schritte:

1. **API erkunden:** Swagger UI (http://localhost:5000)
2. **Code verstehen:** Controller durchgehen
3. **Blazor integrieren:** BLAZOR-API-MIGRATION.md lesen
4. **Features entwickeln:** PROJECT-STATUS.md → Roadmap

**Happy Coding! 🚀**

---

*NotenPro v1.0 | HTL Krems | Dezember 2024*
