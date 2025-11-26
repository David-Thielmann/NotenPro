-- ============================================
-- NotenPro Database Setup Script
-- HTL Krems Notenverwaltungssystem
-- ============================================

-- 1. Datenbank erstellen
DROP DATABASE IF EXISTS notenpro_db;
CREATE DATABASE notenpro_db 
  CHARACTER SET utf8mb4 
  COLLATE utf8mb4_unicode_ci;

USE notenpro_db;

-- 2. Benutzer erstellen (Optional - für Production)
-- Für Development kann root verwendet werden
DROP USER IF EXISTS 'notenpro_user'@'localhost';
CREATE USER 'notenpro_user'@'localhost' 
  IDENTIFIED BY 'SecurePassword123!';

GRANT ALL PRIVILEGES ON notenpro_db.* 
  TO 'notenpro_user'@'localhost';

FLUSH PRIVILEGES;

-- 3. Prüfen
SELECT 'Database Setup Complete!' AS Status;
SHOW DATABASES LIKE 'notenpro%';
SELECT user, host FROM mysql.user WHERE user LIKE 'notenpro%';

-- ============================================
-- Hinweis: Die Tabellen werden automatisch
-- durch Entity Framework Core Migrations erstellt!
-- 
-- Einfach ausführen: dotnet run
-- ============================================

-- Optional: Datenbank komplett zurücksetzen
-- DROP DATABASE IF EXISTS notenpro_db;
-- DROP USER IF EXISTS 'notenpro_user'@'localhost';
