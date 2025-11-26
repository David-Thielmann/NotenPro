# NotenPro API - Komplette Setup-Anleitung

## 📦 Schritt-für-Schritt Installation

### 1. Voraussetzungen prüfen

```bash
# .NET 9 SDK installiert?
dotnet --version
# Sollte 9.0.x ausgeben

# MySQL installiert?
mysql --version
# Sollte MySQL 8.0+ oder MariaDB 10.5+ sein
```

### 2. MySQL Datenbank vorbereiten

#### Option A: Mit MySQL Workbench
1. MySQL Workbench öffnen
2. Neue Verbindung zu localhost erstellen
3. SQL-Tab öffnen und ausführen:

```sql
-- Datenbank erstellen
CREATE DATABASE notenpro_db 
  CHARACTER SET utf8mb4 
  COLLATE utf8mb4_unicode_ci;

-- Benutzer erstellen (optional, empfohlen für Production)
CREATE USER 'notenpro_user'@'localhost' 
  IDENTIFIED BY 'SecurePassword123!';

GRANT ALL PRIVILEGES ON notenpro_db.* 
  TO 'notenpro_user'@'localhost';

FLUSH PRIVILEGES;

-- Prüfen
SHOW DATABASES;
SELECT user, host FROM mysql.user WHERE user = 'notenpro_user';
```

#### Option B: Mit MySQL Command Line
```bash
mysql -u root -p
```

Dann die obigen SQL-Befehle ausführen.

### 3. Connection String konfigurieren

Öffne `appsettings.Development.json` und passe an:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=notenpro_db;User=root;Password=DEIN_ROOT_PASSWORT;"
  }
}
```

**Wichtig:** Ersetze `DEIN_ROOT_PASSWORT` mit deinem MySQL root-Passwort!

Für Production (mit dediziertem User):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=notenpro_db;User=notenpro_user;Password=SecurePassword123!;SslMode=Preferred;"
  }
}
```

### 4. NuGet Packages installieren

```bash
cd NotenPro.Api
dotnet restore
```

Das installiert:
- Microsoft.EntityFrameworkCore.Design (9.0.0)
- Pomelo.EntityFrameworkCore.MySql (9.0.0)
- Swashbuckle.AspNetCore (7.2.0)
- BCrypt.Net-Next (4.0.3)
- Microsoft.AspNetCore.Authentication.JwtBearer (9.0.0)

### 5. Projekt kompilieren

```bash
dotnet build
```

Sollte ohne Fehler durchlaufen.

### 6. Entity Framework Tools installieren (falls nicht vorhanden)

```bash
dotnet tool install --global dotnet-ef
# oder updaten:
dotnet tool update --global dotnet-ef
```

### 7. Datenbank-Migration

#### Option A: Automatisch beim Start (empfohlen)
```bash
dotnet run
```

Die Datenbank wird automatisch erstellt und mit Seed-Daten gefüllt.

#### Option B: Manuell mit EF Core Tools
```bash
# Migration erstellen
dotnet ef migrations add InitialCreate

# Datenbank erstellen
dotnet ef database update

# Projekt starten
dotnet run
```

### 8. API testen

Nach dem Start solltest du sehen:

```
====================================
   NotenPro API v1.0
   HTL Krems Notenverwaltung
====================================
Environment: Development
Swagger UI: http://localhost:5000
====================================

Default Login Credentials:
----------------------------------
System Admin:
  Email: sysadmin@notenpro.at
  Password: Admin@123
...
```

Öffne Browser: **http://localhost:5000**

Du solltest die Swagger UI sehen!

### 9. Login testen

#### Mit Swagger UI:
1. Öffne `http://localhost:5000`
2. Suche `POST /api/auth/login`
3. Klicke "Try it out"
4. Request Body:
```json
{
  "email": "sysadmin@notenpro.at",
  "password": "Admin@123"
}
```
5. Klicke "Execute"
6. Response sollte sein:
```json
{
  "success": true,
  "user": {
    "id": "...",
    "name": "System Administrator",
    "email": "sysadmin@notenpro.at",
    "role": "SystemAdmin",
    ...
  },
  "token": "mock-jwt-token-..."
}
```

#### Mit cURL:
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"sysadmin@notenpro.at","password":"Admin@123"}'
```

#### Mit PowerShell:
```powershell
$body = @{
    email = "sysadmin@notenpro.at"
    password = "Admin@123"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/auth/login" `
  -Method Post `
  -Body $body `
  -ContentType "application/json"
```

### 10. Weitere Endpoints testen

**Alle Schulen abrufen:**
```bash
curl http://localhost:5000/api/schools
```

**Alle Benutzer:**
```bash
curl http://localhost:5000/api/users
```

**Noten eines Schülers:**
```bash
# Zuerst Schüler-ID aus /api/users?role=Student holen
curl "http://localhost:5000/api/grades/student/{STUDENT_ID}"
```

## 🔧 Häufige Probleme & Lösungen

### Problem: "Unable to connect to any of the specified MySQL hosts"

**Lösung 1:** MySQL Server starten
```bash
# Windows
net start MySQL80

# Linux/Mac
sudo systemctl start mysql
```

**Lösung 2:** Connection String prüfen
- Ist der Port korrekt? (Standard: 3306)
- Ist das Passwort korrekt?
- Läuft MySQL auf localhost?

### Problem: "Authentication method 'caching_sha2_password' failed"

**Lösung:** MySQL User auth method ändern
```sql
ALTER USER 'root'@'localhost' 
  IDENTIFIED WITH mysql_native_password 
  BY 'dein_passwort';
FLUSH PRIVILEGES;
```

### Problem: "Build failed" - Compilier-Fehler

**Lösung:** Packages neu installieren
```bash
dotnet clean
dotnet restore
dotnet build
```

### Problem: Migration-Fehler

**Lösung:** Alles zurücksetzen
```bash
# Datenbank löschen
dotnet ef database drop --force

# Alte Migrations entfernen
dotnet ef migrations remove

# Neu erstellen
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Problem: "Port 5000 already in use"

**Lösung 1:** Port in launchSettings.json ändern
```json
{
  "applicationUrl": "http://localhost:5555"
}
```

**Lösung 2:** Process beenden
```bash
# Windows
netstat -ano | findstr :5000
taskkill /PID <PID> /F

# Linux/Mac
lsof -i :5000
kill -9 <PID>
```

## 📊 Datenbank-Struktur prüfen

Nach erfolgreicher Migration:

```sql
USE notenpro_db;

-- Alle Tabellen anzeigen
SHOW TABLES;

-- Sollte zeigen:
-- classes
-- early_warnings
-- grades
-- notifications
-- schools
-- student_classes
-- subjects
-- teacher_subjects
-- tests
-- users
-- __EFMigrationsHistory

-- Seed-Daten prüfen
SELECT * FROM schools;
SELECT email, role FROM users;
SELECT name FROM classes;
SELECT name FROM subjects;
```

## 🚀 Production Deployment

### 1. appsettings.Production.json erstellen

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=PRODUCTION_SERVER;Port=3306;Database=notenpro_db;User=notenpro_user;Password=STRONG_PASSWORD;SslMode=Required;"
  }
}
```

### 2. Publishen

```bash
dotnet publish -c Release -o ./publish
```

### 3. Als Service einrichten

#### Windows (IIS)
1. IIS Manager öffnen
2. Neue Site erstellen
3. Physical path: `./publish`
4. .NET Core Hosting Bundle installieren

#### Linux (systemd)
```bash
sudo nano /etc/systemd/system/notenpro-api.service
```

```ini
[Unit]
Description=NotenPro API

[Service]
WorkingDirectory=/var/www/notenpro-api
ExecStart=/usr/bin/dotnet /var/www/notenpro-api/NotenPro.Api.dll
Restart=always
RestartSec=10
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

```bash
sudo systemctl enable notenpro-api
sudo systemctl start notenpro-api
sudo systemctl status notenpro-api
```

## 📝 Development Workflow

### Neue Entity hinzufügen

1. Entity-Klasse in `Data/Entities/` erstellen
2. DbSet in `NotenProDbContext.cs` hinzufügen
3. ModelBuilder Konfiguration
4. Migration erstellen:
```bash
dotnet ef migrations add AddNewEntity
dotnet ef database update
```
5. DTO erstellen in `DTOs/`
6. Controller erstellen in `Controllers/`

### Code-Änderungen testen

```bash
# Projekt neu builden
dotnet build

# Tests ausführen (wenn vorhanden)
dotnet test

# Projekt starten
dotnet run

# Im Browser: http://localhost:5000
```

## 🔍 Nützliche Befehle

```bash
# Alle Migrations anzeigen
dotnet ef migrations list

# Zu bestimmter Migration zurück
dotnet ef database update MigrationName

# SQL-Script für Migration generieren
dotnet ef migrations script

# Projekt watch mode (auto-reload)
dotnet watch run

# Detaillierte Logs
dotnet run --verbosity detailed
```

## ✅ Setup-Checkliste

- [ ] .NET 9 SDK installiert
- [ ] MySQL Server läuft
- [ ] Datenbank `notenpro_db` erstellt
- [ ] Connection String konfiguriert
- [ ] `dotnet restore` ausgeführt
- [ ] `dotnet build` erfolgreich
- [ ] Datenbank migriert
- [ ] `dotnet run` startet ohne Fehler
- [ ] Swagger UI erreichbar (http://localhost:5000)
- [ ] Login funktioniert
- [ ] Seed-Daten vorhanden

## 🎉 Fertig!

Deine NotenPro API läuft jetzt!

**Nächste Schritte:**
1. Blazor WebAssembly Client mit API verbinden
2. Authentifizierung mit echten JWT-Tokens
3. Weitere Features implementieren

Bei Fragen oder Problemen: [Issue erstellen]
