# 🚀 NotenPro API - QuickStart (5 Minuten)

## Schnellstart für Entwickler

### 1️⃣ MySQL vorbereiten (1 Minute)

```bash
mysql -u root -p
```

```sql
CREATE DATABASE notenpro_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
EXIT;
```

### 2️⃣ Connection String einstellen (30 Sekunden)

Datei: `appsettings.Development.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=notenpro_db;User=root;Password=DEIN_PASSWORT;"
  }
}
```

**Wichtig:** `DEIN_PASSWORT` durch dein MySQL root-Passwort ersetzen!

### 3️⃣ API starten (1 Minute)

```bash
cd NotenPro.Api
dotnet restore
dotnet run
```

### 4️⃣ Testen (30 Sekunden)

Browser öffnen: **http://localhost:5000**

Du siehst die Swagger UI! ✅

### 5️⃣ Login testen (1 Minute)

In Swagger UI:
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

✅ **Fertig!** Die API läuft!

---

## 📋 Demo-Accounts

| Email | Password | Rolle |
|-------|----------|-------|
| sysadmin@notenpro.at | Admin@123 | System Admin |
| admin@htl-krems.ac.at | Admin@123 | School Admin |
| maria.schmidt@htl-krems.ac.at | Teacher@123 | Lehrer |
| max.mustermann@students.htl-krems.ac.at | Student@123 | Schüler |

---

## 🔍 Wichtige Endpoints

```bash
# Alle Schulen
http://localhost:5000/api/schools

# Alle Benutzer
http://localhost:5000/api/users

# Alle Klassen
http://localhost:5000/api/classes

# Alle Fächer
http://localhost:5000/api/subjects

# Tests eines Lehrers
http://localhost:5000/api/tests/teacher/{teacherId}

# Noten eines Schülers
http://localhost:5000/api/grades/student/{studentId}
```

---

## ⚠️ Troubleshooting

### MySQL Connection Error?
```bash
# MySQL starten
net start MySQL80  # Windows
sudo systemctl start mysql  # Linux
```

### Port 5000 belegt?
Ändere in `Properties/launchSettings.json`:
```json
"applicationUrl": "http://localhost:5555"
```

### Build-Fehler?
```bash
dotnet clean
dotnet restore
dotnet build
```

---

## 📚 Mehr Infos

- **Vollständige Anleitung:** [SETUP.md](SETUP.md)
- **API Dokumentation:** [README.md](README.md)
- **Swagger UI:** http://localhost:5000

---

## ✅ Das war's!

🎉 **Deine NotenPro API läuft in unter 5 Minuten!**

Nächster Schritt: Blazor Client mit API verbinden
