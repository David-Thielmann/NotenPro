# 📝 NotenPro API - Request Examples

Praktische Beispiele für alle wichtigen API-Operationen.

---

## 🔐 1. Authentication

### Login als System Admin
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "sysadmin@notenpro.at",
    "password": "Admin@123"
  }'
```

**Response:**
```json
{
  "success": true,
  "user": {
    "id": "abc123...",
    "name": "System Administrator",
    "email": "sysadmin@notenpro.at",
    "role": "SystemAdmin",
    "isActive": true
  },
  "token": "mock-jwt-token-abc123..."
}
```

### Login als Lehrer
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "maria.schmidt@htl-krems.ac.at",
    "password": "Teacher@123"
  }'
```

### Login als Schüler
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "max.mustermann@students.htl-krems.ac.at",
    "password": "Student@123"
  }'
```

---

## 👤 2. Users

### Alle Benutzer abrufen
```bash
curl http://localhost:5000/api/users
```

### Nur Schüler abrufen
```bash
curl "http://localhost:5000/api/users?role=Student"
```

### Nur Lehrer einer Schule
```bash
curl "http://localhost:5000/api/users?role=Teacher&schoolId={SCHOOL_ID}"
```

### Neuen Schüler erstellen
```bash
curl -X POST http://localhost:5000/api/users \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Anna Müller",
    "email": "anna.mueller@students.htl-krems.ac.at",
    "password": "Student@123",
    "role": "Student",
    "schoolId": "{SCHOOL_ID}"
  }'
```

### Neuen Lehrer erstellen
```bash
curl -X POST http://localhost:5000/api/users \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Prof. Johann Wagner",
    "email": "johann.wagner@htl-krems.ac.at",
    "password": "Teacher@123",
    "role": "Teacher",
    "schoolId": "{SCHOOL_ID}"
  }'
```

### Benutzer aktualisieren
```bash
curl -X PUT http://localhost:5000/api/users/{USER_ID} \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Anna Müller-Schmidt",
    "email": "anna.mueller@students.htl-krems.ac.at",
    "isActive": true
  }'
```

### Passwort ändern
```bash
curl -X PUT http://localhost:5000/api/users/{USER_ID}/password \
  -H "Content-Type: application/json" \
  -d '{
    "newPassword": "NewSecurePassword@456"
  }'
```

### Benutzer-Statistiken
```bash
curl http://localhost:5000/api/users/{USER_ID}/statistics
```

---

## 🏫 3. Schools

### Alle Schulen abrufen
```bash
curl http://localhost:5000/api/schools
```

### Neue Schule erstellen
```bash
curl -X POST http://localhost:5000/api/schools \
  -H "Content-Type: application/json" \
  -d '{
    "name": "HTL Mödling",
    "location": "Mödling, Niederösterreich"
  }'
```

### Schule aktualisieren
```bash
curl -X PUT http://localhost:5000/api/schools/{SCHOOL_ID} \
  -H "Content-Type: application/json" \
  -d '{
    "name": "HTL Mödling",
    "location": "Mödling, NÖ",
    "status": "Active"
  }'
```

### Schul-Statistiken
```bash
curl http://localhost:5000/api/schools/{SCHOOL_ID}/statistics
```

---

## 📚 4. Classes

### Alle Klassen einer Schule
```bash
curl "http://localhost:5000/api/classes?schoolId={SCHOOL_ID}"
```

### Neue Klasse erstellen
```bash
curl -X POST http://localhost:5000/api/classes \
  -H "Content-Type: application/json" \
  -d '{
    "name": "5CHIT",
    "schoolId": "{SCHOOL_ID}",
    "classTeacherId": "{TEACHER_ID}"
  }'
```

### Schüler einer Klasse abrufen
```bash
curl http://localhost:5000/api/classes/{CLASS_ID}/students
```

### Schüler zu Klasse hinzufügen
```bash
curl -X POST http://localhost:5000/api/classes/{CLASS_ID}/students/{STUDENT_ID}
```

### Schüler aus Klasse entfernen
```bash
curl -X DELETE http://localhost:5000/api/classes/{CLASS_ID}/students/{STUDENT_ID}
```

---

## 📖 5. Subjects

### Alle Fächer einer Schule
```bash
curl "http://localhost:5000/api/subjects?schoolId={SCHOOL_ID}"
```

### Fächer eines Lehrers
```bash
curl http://localhost:5000/api/subjects/teacher/{TEACHER_ID}
```

### Neues Fach erstellen
```bash
curl -X POST http://localhost:5000/api/subjects \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Datenbanken",
    "description": "Datenbanksysteme und SQL",
    "schoolId": "{SCHOOL_ID}"
  }'
```

### Lehrer zu Fach zuweisen
```bash
curl -X POST http://localhost:5000/api/subjects/{SUBJECT_ID}/teachers/{TEACHER_ID}
```

### Lehrer von Fach entfernen
```bash
curl -X DELETE http://localhost:5000/api/subjects/{SUBJECT_ID}/teachers/{TEACHER_ID}
```

---

## 📝 6. Tests

### Alle Tests eines Lehrers
```bash
curl http://localhost:5000/api/tests/teacher/{TEACHER_ID}
```

### Tests einer Klasse
```bash
curl http://localhost:5000/api/tests/class/{CLASS_ID}
```

### Neuen Test erstellen
```bash
curl -X POST "http://localhost:5000/api/tests?teacherId={TEACHER_ID}" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Schularbeit Analysis",
    "subjectId": "{SUBJECT_ID}",
    "classId": "{CLASS_ID}",
    "date": "2024-12-15T10:00:00Z",
    "maxPoints": 100,
    "type": "Schularbeit",
    "description": "Integralrechnung und Differentialrechnung"
  }'
```

**Hinweis:** Bei Test-Erstellung werden automatisch Noten (Status: Pending) für alle Schüler der Klasse angelegt!

### Test aktualisieren
```bash
curl -X PUT http://localhost:5000/api/tests/{TEST_ID} \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Schularbeit Analysis (Update)",
    "date": "2024-12-16T10:00:00Z",
    "maxPoints": 120,
    "type": "Schularbeit",
    "description": "Erweitert um Kurvendiskussion"
  }'
```

### Test löschen
```bash
curl -X DELETE http://localhost:5000/api/tests/{TEST_ID}
```

---

## 🎓 7. Grades

### Noten eines Schülers
```bash
curl http://localhost:5000/api/grades/student/{STUDENT_ID}
```

### Noten eines Tests
```bash
curl http://localhost:5000/api/grades/test/{TEST_ID}
```

### Einzelne Note erstellen
```bash
curl -X POST http://localhost:5000/api/grades \
  -H "Content-Type: application/json" \
  -d '{
    "studentId": "{STUDENT_ID}",
    "testId": "{TEST_ID}",
    "gradeValue": 2.0,
    "points": 85,
    "status": "Graded",
    "comment": "Sehr gute Leistung!"
  }'
```

**Hinweis:** Bei Note-Eintragung wird automatisch eine Benachrichtigung an den Schüler gesendet!

### Noten für ganze Klasse (Bulk)
```bash
curl -X POST http://localhost:5000/api/grades/bulk \
  -H "Content-Type: application/json" \
  -d '{
    "testId": "{TEST_ID}",
    "grades": [
      {
        "studentId": "{STUDENT_1_ID}",
        "gradeValue": 1.0,
        "points": 95,
        "status": "Graded",
        "comment": "Ausgezeichnet!"
      },
      {
        "studentId": "{STUDENT_2_ID}",
        "gradeValue": 2.0,
        "points": 85,
        "status": "Graded",
        "comment": "Sehr gut!"
      },
      {
        "studentId": "{STUDENT_3_ID}",
        "gradeValue": 3.0,
        "points": 70,
        "status": "Graded"
      },
      {
        "studentId": "{STUDENT_4_ID}",
        "status": "Absent"
      }
    ]
  }'
```

### Note aktualisieren
```bash
curl -X PUT http://localhost:5000/api/grades/{GRADE_ID} \
  -H "Content-Type: application/json" \
  -d '{
    "gradeValue": 1.0,
    "points": 92,
    "status": "Graded",
    "comment": "Nachträglich verbessert!"
  }'
```

---

## 🔔 8. Notifications

### Benachrichtigungen eines Benutzers
```bash
curl http://localhost:5000/api/notifications/user/{USER_ID}
```

### Nur ungelesene Benachrichtigungen
```bash
curl http://localhost:5000/api/notifications/user/{USER_ID}/unread
```

### Anzahl ungelesener Benachrichtigungen
```bash
curl http://localhost:5000/api/notifications/user/{USER_ID}/count
```

### Neue Benachrichtigung erstellen
```bash
curl -X POST http://localhost:5000/api/notifications \
  -H "Content-Type: application/json" \
  -d '{
    "userId": "{USER_ID}",
    "title": "Wichtige Information",
    "message": "Die nächste Schularbeit findet am 15.12. statt.",
    "type": "Info"
  }'
```

### Benachrichtigung an mehrere Benutzer (Broadcast)
```bash
curl -X POST http://localhost:5000/api/notifications/broadcast \
  -H "Content-Type: application/json" \
  -d '{
    "userIds": ["{USER_1_ID}", "{USER_2_ID}", "{USER_3_ID}"],
    "title": "Schulveranstaltung",
    "message": "Am Freitag findet ein Sportfest statt.",
    "type": "Info"
  }'
```

### Als gelesen markieren
```bash
curl -X PUT http://localhost:5000/api/notifications/{NOTIFICATION_ID}/read
```

### Mehrere als gelesen markieren
```bash
curl -X POST http://localhost:5000/api/notifications/mark-read \
  -H "Content-Type: application/json" \
  -d '{
    "notificationIds": ["{ID_1}", "{ID_2}", "{ID_3}"]
  }'
```

### Alle als gelesen markieren
```bash
curl -X POST http://localhost:5000/api/notifications/user/{USER_ID}/mark-all-read
```

### Alle Benachrichtigungen löschen
```bash
curl -X DELETE http://localhost:5000/api/notifications/user/{USER_ID}/clear
```

---

## ⚠️ 9. Early Warnings (Frühwarnungen)

### Alle Frühwarnungen eines Lehrers
```bash
curl http://localhost:5000/api/earlywarnings/teacher/{TEACHER_ID}
```

### Frühwarnungen eines Schülers
```bash
curl http://localhost:5000/api/earlywarnings/student/{STUDENT_ID}
```

### Ausstehende (noch nicht versendete) Frühwarnungen
```bash
curl http://localhost:5000/api/earlywarnings/pending
```

### Neue Frühwarnung erstellen
```bash
curl -X POST "http://localhost:5000/api/earlywarnings?teacherId={TEACHER_ID}" \
  -H "Content-Type: application/json" \
  -d '{
    "studentId": "{STUDENT_ID}",
    "subjectId": "{SUBJECT_ID}",
    "reason": "Mehrfaches Fehlen bei Tests und unzureichende Mitarbeit",
    "currentAverage": 4.2
  }'
```

### Frühwarnungen versenden
```bash
curl -X POST http://localhost:5000/api/earlywarnings/send \
  -H "Content-Type: application/json" \
  -d '{
    "warningIds": ["{WARNING_1_ID}", "{WARNING_2_ID}"]
  }'
```

**Hinweis:** Beim Versenden werden automatisch Benachrichtigungen an die Schüler gesendet!

### Frühwarn-Statistiken für ein Fach
```bash
curl http://localhost:5000/api/earlywarnings/statistics/subject/{SUBJECT_ID}
```

---

## 🔍 10. Komplexe Queries

### Alle Schüler einer Klasse mit ihren Noten
```bash
# 1. Schüler der Klasse holen
curl "http://localhost:5000/api/users?role=Student&classId={CLASS_ID}"

# 2. Für jeden Schüler Noten holen
curl http://localhost:5000/api/grades/student/{STUDENT_ID}
```

### Durchschnittsnote eines Schülers berechnen
```bash
# Alle Noten holen
curl http://localhost:5000/api/grades/student/{STUDENT_ID}

# Im Frontend: grades.filter(g => g.status === "Graded").average(g => g.gradeValue)
```

### Tests mit Korrektur-Status
```bash
# Tests eines Lehrers
curl http://localhost:5000/api/tests/teacher/{TEACHER_ID}

# Response enthält gradedCount und totalStudents pro Test
```

---

## 💡 Tipps

### IDs herausfinden

1. **Nach Login:** User-ID ist in der Response
2. **Schulen:** `GET /api/schools` gibt alle IDs
3. **Klassen:** `GET /api/classes?schoolId={ID}`
4. **Fächer:** `GET /api/subjects?schoolId={ID}`
5. **Tests:** `GET /api/tests/teacher/{ID}`

### Workflow: Neuer Test mit Noten

```bash
# 1. Test erstellen (erstellt automatisch Pending-Noten)
curl -X POST "http://localhost:5000/api/tests?teacherId={T_ID}" -d '{...}'

# 2. Test-ID aus Response nehmen

# 3. Noten für alle Schüler eintragen (Bulk)
curl -X POST http://localhost:5000/api/grades/bulk -d '{
  "testId": "{TEST_ID}",
  "grades": [...]
}'

# 4. Schüler erhalten automatisch Benachrichtigungen!
```

### PowerShell statt cURL

```powershell
# Login
$body = @{ email = "user@example.com"; password = "password" } | ConvertTo-Json
Invoke-RestMethod -Uri "http://localhost:5000/api/auth/login" -Method Post -Body $body -ContentType "application/json"

# GET Request
Invoke-RestMethod -Uri "http://localhost:5000/api/schools"

# POST mit Token
$headers = @{ Authorization = "Bearer {TOKEN}" }
Invoke-RestMethod -Uri "http://localhost:5000/api/..." -Method Post -Headers $headers -Body $body -ContentType "application/json"
```

### Postman Collection

Import in Postman:
1. File → Import → Link
2. `http://localhost:5000/swagger/v1/swagger.json`
3. Alle Endpoints werden automatisch importiert!

---

## 📚 Weitere Beispiele

Siehe auch:
- **Swagger UI:** http://localhost:5000 (interaktive Dokumentation)
- **SETUP.md:** Detaillierte Setup-Anleitung
- **README.md:** Vollständige API-Dokumentation

---

**Happy Coding! 🚀**
