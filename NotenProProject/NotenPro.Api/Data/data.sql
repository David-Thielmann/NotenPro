-- =========================================
-- 0) Optional: Clean (nur wenn du neu befüllen willst)
-- =========================================
 DELETE FROM grades;
 DELETE FROM early_warnings;
 DELETE FROM notifications;
DELETE FROM student_classes;
 DELETE FROM teacher_subjects;
 DELETE FROM tests;
 DELETE FROM classes;
 DELETE FROM subjects;
 DELETE FROM users;
 DELETE FROM schools;

-- =========================================
-- 1) Schools
-- =========================================
INSERT INTO schools (id, name, location, status, created_at, updated_at) VALUES
                                                                             ('11111111-1111-1111-1111-111111111111', 'HTL Krems', 'Krems an der Donau', 'Active', UTC_TIMESTAMP(6), UTC_TIMESTAMP(6)),
                                                                             ('33333333-3333-3333-3333-333333333333', 'HTL Wien',  'Wien',              'Active', UTC_TIMESTAMP(6), UTC_TIMESTAMP(6));

-- =========================================
-- 2) Users (Teacher + Students)
-- role: 0=Student, 1=Teacher, 2=Admin, 3=SysAdmin (laut deinem Seed)
-- =========================================

-- Krems Teachers
INSERT INTO users (id, name, email, password_hash, role, school_id, is_active, created_at, updated_at, external_id) VALUES
                                                                                                                        ('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1', 'Prof. Max Lehrer',     'max.lehrer@htl-krems.ac.at', NULL, 1, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2', 'Prof. Anna Deutsch',   'anna.deutsch@htl-krems.ac.at', NULL, 1, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3', 'Prof. Tom Netzwerk',   'tom.netzwerk@htl-krems.ac.at', NULL, 1, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), '');

-- Wien Teachers
INSERT INTO users (id, name, email, password_hash, role, school_id, is_active, created_at, updated_at, external_id) VALUES
                                                                                                                        ('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4', 'Prof. Wien Mathe',     'wien.mathe@htl-wien.ac.at', NULL, 1, '33333333-3333-3333-3333-333333333333', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5', 'Prof. Wien Prog',      'wien.prog@htl-wien.ac.at', NULL, 1, '33333333-3333-3333-3333-333333333333', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), '');

-- Krems Students (12)
INSERT INTO users (id, name, email, password_hash, role, school_id, is_active, created_at, updated_at, external_id) VALUES
                                                                                                                        ('c0000000-0000-0000-0000-000000000001', 'Schüler 01', 's01@students.htl-krems.ac.at', NULL, 0, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('c0000000-0000-0000-0000-000000000002', 'Schüler 02', 's02@students.htl-krems.ac.at', NULL, 0, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('c0000000-0000-0000-0000-000000000003', 'Schüler 03', 's03@students.htl-krems.ac.at', NULL, 0, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('c0000000-0000-0000-0000-000000000004', 'Schüler 04', 's04@students.htl-krems.ac.at', NULL, 0, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('c0000000-0000-0000-0000-000000000005', 'Schüler 05', 's05@students.htl-krems.ac.at', NULL, 0, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('c0000000-0000-0000-0000-000000000006', 'Schüler 06', 's06@students.htl-krems.ac.at', NULL, 0, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('c0000000-0000-0000-0000-000000000007', 'Schüler 07', 's07@students.htl-krems.ac.at', NULL, 0, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('c0000000-0000-0000-0000-000000000008', 'Schüler 08', 's08@students.htl-krems.ac.at', NULL, 0, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('c0000000-0000-0000-0000-000000000009', 'Schüler 09', 's09@students.htl-krems.ac.at', NULL, 0, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('c0000000-0000-0000-0000-000000000010', 'Schüler 10', 's10@students.htl-krems.ac.at', NULL, 0, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('c0000000-0000-0000-0000-000000000011', 'Schüler 11', 's11@students.htl-krems.ac.at', NULL, 0, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('c0000000-0000-0000-0000-000000000012', 'Schüler 12', 's12@students.htl-krems.ac.at', NULL, 0, '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), '');

-- Wien Students (6)
INSERT INTO users (id, name, email, password_hash, role, school_id, is_active, created_at, updated_at, external_id) VALUES
                                                                                                                        ('d0000000-0000-0000-0000-000000000101', 'Wien Schüler 01', 'w01@students.htl-wien.ac.at', NULL, 0, '33333333-3333-3333-3333-333333333333', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('d0000000-0000-0000-0000-000000000102', 'Wien Schüler 02', 'w02@students.htl-wien.ac.at', NULL, 0, '33333333-3333-3333-3333-333333333333', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('d0000000-0000-0000-0000-000000000103', 'Wien Schüler 03', 'w03@students.htl-wien.ac.at', NULL, 0, '33333333-3333-3333-3333-333333333333', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('d0000000-0000-0000-0000-000000000104', 'Wien Schüler 04', 'w04@students.htl-wien.ac.at', NULL, 0, '33333333-3333-3333-3333-333333333333', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('d0000000-0000-0000-0000-000000000105', 'Wien Schüler 05', 'w05@students.htl-wien.ac.at', NULL, 0, '33333333-3333-3333-3333-333333333333', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), ''),
                                                                                                                        ('d0000000-0000-0000-0000-000000000106', 'Wien Schüler 06', 'w06@students.htl-wien.ac.at', NULL, 0, '33333333-3333-3333-3333-333333333333', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6), '');

-- =========================================
-- 3) Subjects
-- =========================================
INSERT INTO subjects (id, name, description, school_id, is_active, created_at, updated_at) VALUES
                                                                                               ('e1000000-0000-0000-0000-000000000001', 'Mathematik',    'Angewandte Mathematik',          '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6)),
                                                                                               ('e1000000-0000-0000-0000-000000000002', 'Deutsch',       'Deutsche Sprache und Literatur', '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6)),
                                                                                               ('e1000000-0000-0000-0000-000000000003', 'Englisch',      'English Language',               '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6)),
                                                                                               ('e1000000-0000-0000-0000-000000000004', 'Programmieren', 'Software Engineering',           '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6)),
                                                                                               ('e1000000-0000-0000-0000-000000000005', 'Netzwerke',     'Netzwerktechnik Grundlagen',     '11111111-1111-1111-1111-111111111111', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6)),
                                                                                               ('e3000000-0000-0000-0000-000000000001', 'Mathematik',    'Mathematik (Wien)',              '33333333-3333-3333-3333-333333333333', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6)),
                                                                                               ('e3000000-0000-0000-0000-000000000002', 'Programmieren', 'Programmieren (Wien)',           '33333333-3333-3333-3333-333333333333', 1, UTC_TIMESTAMP(6), UTC_TIMESTAMP(6));

-- =========================================
-- 4) Classes
-- class_teacher_id muss existieren in users oder NULL sein
-- =========================================
INSERT INTO classes (id, name, school_id, class_teacher_id, created_at, updated_at) VALUES
                                                                                        ('f1111111-1111-1111-1111-111111111101', '5AHIT', '11111111-1111-1111-1111-111111111111', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1', UTC_TIMESTAMP(6), UTC_TIMESTAMP(6)),
                                                                                        ('f1111111-1111-1111-1111-111111111102', '5BHIT', '11111111-1111-1111-1111-111111111111', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2', UTC_TIMESTAMP(6), UTC_TIMESTAMP(6)),
                                                                                        ('f1111111-1111-1111-1111-111111111103', '4AHIT', '11111111-1111-1111-1111-111111111111', NULL,                                   UTC_TIMESTAMP(6), UTC_TIMESTAMP(6)),
                                                                                        ('f3333333-3333-3333-3333-333333333201', '5AHIT', '33333333-3333-3333-3333-333333333333', 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4', UTC_TIMESTAMP(6), UTC_TIMESTAMP(6));

-- =========================================
-- 5) TeacherSubjects
-- =========================================
INSERT INTO teacher_subjects (id, teacher_id, subject_id, assigned_at) VALUES
                                                                           ('11111111-2222-2222-2222-222222222221', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1', 'e1000000-0000-0000-0000-000000000001', UTC_TIMESTAMP(6)),
                                                                           ('11111111-2222-2222-2222-222222222222', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1', 'e1000000-0000-0000-0000-000000000004', UTC_TIMESTAMP(6)),
                                                                           ('11111111-2222-2222-2222-222222222223', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2', 'e1000000-0000-0000-0000-000000000002', UTC_TIMESTAMP(6)),
                                                                           ('11111111-2222-2222-2222-222222222224', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2', 'e1000000-0000-0000-0000-000000000003', UTC_TIMESTAMP(6)),
                                                                           ('11111111-2222-2222-2222-222222222225', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3', 'e1000000-0000-0000-0000-000000000005', UTC_TIMESTAMP(6)),
                                                                           ('33333333-2222-2222-2222-222222222221', 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4', 'e3000000-0000-0000-0000-000000000001', UTC_TIMESTAMP(6)),
                                                                           ('33333333-2222-2222-2222-222222222222', 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5', 'e3000000-0000-0000-0000-000000000002', UTC_TIMESTAMP(6));

-- =========================================
-- 6) StudentClasses
-- =========================================
-- Krems: 5AHIT (8)
INSERT INTO student_classes (id, student_id, class_id, enrolled_at) VALUES
                                                                        ('20000000-0000-0000-0000-000000000001','c0000000-0000-0000-0000-000000000001','f1111111-1111-1111-1111-111111111101',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000002','c0000000-0000-0000-0000-000000000002','f1111111-1111-1111-1111-111111111101',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000003','c0000000-0000-0000-0000-000000000003','f1111111-1111-1111-1111-111111111101',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000004','c0000000-0000-0000-0000-000000000004','f1111111-1111-1111-1111-111111111101',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000005','c0000000-0000-0000-0000-000000000005','f1111111-1111-1111-1111-111111111101',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000006','c0000000-0000-0000-0000-000000000006','f1111111-1111-1111-1111-111111111101',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000007','c0000000-0000-0000-0000-000000000007','f1111111-1111-1111-1111-111111111101',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000008','c0000000-0000-0000-0000-000000000008','f1111111-1111-1111-1111-111111111101',UTC_TIMESTAMP(6));

-- Krems: 5BHIT (4)
INSERT INTO student_classes (id, student_id, class_id, enrolled_at) VALUES
                                                                        ('20000000-0000-0000-0000-000000000009','c0000000-0000-0000-0000-000000000009','f1111111-1111-1111-1111-111111111102',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000010','c0000000-0000-0000-0000-000000000010','f1111111-1111-1111-1111-111111111102',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000011','c0000000-0000-0000-0000-000000000011','f1111111-1111-1111-1111-111111111102',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000012','c0000000-0000-0000-0000-000000000012','f1111111-1111-1111-1111-111111111102',UTC_TIMESTAMP(6));

-- Wien: 5AHIT (6)
INSERT INTO student_classes (id, student_id, class_id, enrolled_at) VALUES
                                                                        ('20000000-0000-0000-0000-000000000101','d0000000-0000-0000-0000-000000000101','f3333333-3333-3333-3333-333333333201',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000102','d0000000-0000-0000-0000-000000000102','f3333333-3333-3333-3333-333333333201',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000103','d0000000-0000-0000-0000-000000000103','f3333333-3333-3333-3333-333333333201',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000104','d0000000-0000-0000-0000-000000000104','f3333333-3333-3333-3333-333333333201',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000105','d0000000-0000-0000-0000-000000000105','f3333333-3333-3333-3333-333333333201',UTC_TIMESTAMP(6)),
                                                                        ('20000000-0000-0000-0000-000000000106','d0000000-0000-0000-0000-000000000106','f3333333-3333-3333-3333-333333333201',UTC_TIMESTAMP(6));

-- =========================================
-- 7) Tests
-- =========================================
INSERT INTO tests (id, name, subject_id, class_id, teacher_id, date, max_points, type, description, created_at, updated_at) VALUES
                                                                                                                                ('30000000-0000-0000-0000-000000000001','Algebra Test 1','e1000000-0000-0000-0000-000000000001','f1111111-1111-1111-1111-111111111101','aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1',DATE_SUB(UTC_TIMESTAMP(6), INTERVAL 14 DAY),100,0,'Lineare Gleichungen',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                                ('30000000-0000-0000-0000-000000000002','Algebra Test 2','e1000000-0000-0000-0000-000000000001','f1111111-1111-1111-1111-111111111101','aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1',DATE_SUB(UTC_TIMESTAMP(6), INTERVAL 7 DAY), 100,0,'Quadratische Gleichungen',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                                ('30000000-0000-0000-0000-000000000003','C# Basics',    'e1000000-0000-0000-0000-000000000004','f1111111-1111-1111-1111-111111111101','aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1',DATE_SUB(UTC_TIMESTAMP(6), INTERVAL 10 DAY), 50,0,'OOP & Interfaces',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                                ('30000000-0000-0000-0000-000000000004','Deutsch Aufsatz','e1000000-0000-0000-0000-000000000002','f1111111-1111-1111-1111-111111111102','aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2',DATE_SUB(UTC_TIMESTAMP(6), INTERVAL 12 DAY), 40,1,'Erörterung',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                                ('30000000-0000-0000-0000-000000000101','Mathe SA 1',   'e3000000-0000-0000-0000-000000000001','f3333333-3333-3333-3333-333333333201','bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4',DATE_SUB(UTC_TIMESTAMP(6), INTERVAL 20 DAY),100,1,'Schularbeit: Funktionen',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6));

-- =========================================
-- 8) Grades (Algebra Test 1 & 2 + Deutsch)
-- =========================================
INSERT INTO grades (id, student_id, test_id, grade_value, points, max_points, status, comment, created_at, updated_at) VALUES
                                                                                                                           ('40000000-0000-0000-0000-000000000001','c0000000-0000-0000-0000-000000000001','30000000-0000-0000-0000-000000000001',2.00,82,100,0,'Sehr gut!',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000002','c0000000-0000-0000-0000-000000000002','30000000-0000-0000-0000-000000000001',3.00,70,100,0,NULL,UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000003','c0000000-0000-0000-0000-000000000003','30000000-0000-0000-0000-000000000001',4.00,55,100,0,'Mehr üben',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000004','c0000000-0000-0000-0000-000000000004','30000000-0000-0000-0000-000000000001',1.00,95,100,0,'Top!',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000005','c0000000-0000-0000-0000-000000000005','30000000-0000-0000-0000-000000000001',2.00,84,100,0,NULL,UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000006','c0000000-0000-0000-0000-000000000006','30000000-0000-0000-0000-000000000001',5.00,38,100,0,'Aufpassen!',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000007','c0000000-0000-0000-0000-000000000007','30000000-0000-0000-0000-000000000001',3.00,68,100,0,NULL,UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000008','c0000000-0000-0000-0000-000000000008','30000000-0000-0000-0000-000000000001',4.00,58,100,0,NULL,UTC_TIMESTAMP(6),UTC_TIMESTAMP(6));

INSERT INTO grades (id, student_id, test_id, grade_value, points, max_points, status, comment, created_at, updated_at) VALUES
                                                                                                                           ('40000000-0000-0000-0000-000000000009','c0000000-0000-0000-0000-000000000001','30000000-0000-0000-0000-000000000002',3.00,72,100,0,NULL,UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000010','c0000000-0000-0000-0000-000000000002','30000000-0000-0000-0000-000000000002',2.00,86,100,0,'Steigerung!',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000011','c0000000-0000-0000-0000-000000000003','30000000-0000-0000-0000-000000000002',4.00,60,100,0,NULL,UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000012','c0000000-0000-0000-0000-000000000004','30000000-0000-0000-0000-000000000002',1.00,96,100,0,'Sehr gut',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000013','c0000000-0000-0000-0000-000000000005','30000000-0000-0000-0000-000000000002',2.00,88,100,0,NULL,UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000014','c0000000-0000-0000-0000-000000000006','30000000-0000-0000-0000-000000000002',5.00,34,100,0,'Nachhilfe empfohlen',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000015','c0000000-0000-0000-0000-000000000007','30000000-0000-0000-0000-000000000002',3.00,74,100,0,NULL,UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000016','c0000000-0000-0000-0000-000000000008','30000000-0000-0000-0000-000000000002',4.00,59,100,0,NULL,UTC_TIMESTAMP(6),UTC_TIMESTAMP(6));

INSERT INTO grades (id, student_id, test_id, grade_value, points, max_points, status, comment, created_at, updated_at) VALUES
                                                                                                                           ('40000000-0000-0000-0000-000000000021','c0000000-0000-0000-0000-000000000009','30000000-0000-0000-0000-000000000004',2.00,34,40,0,'Guter Aufbau',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000022','c0000000-0000-0000-0000-000000000010','30000000-0000-0000-0000-000000000004',3.00,28,40,0,NULL,UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000023','c0000000-0000-0000-0000-000000000011','30000000-0000-0000-0000-000000000004',4.00,22,40,0,'Mehr Beispiele',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6)),
                                                                                                                           ('40000000-0000-0000-0000-000000000024','c0000000-0000-0000-0000-000000000012','30000000-0000-0000-0000-000000000004',1.00,38,40,0,'Sehr gut',UTC_TIMESTAMP(6),UTC_TIMESTAMP(6));

-- =========================================
-- 9) EarlyWarnings
-- current_average decimal(3,2)
-- =========================================
INSERT INTO early_warnings (id, student_id, subject_id, teacher_id, reason, current_average, is_sent, sent_at, created_at) VALUES
                                                                                                                               ('50000000-0000-0000-0000-000000000001','c0000000-0000-0000-0000-000000000006','e1000000-0000-0000-0000-000000000001','aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1','Leistungen brechen ein, Hausübungen fehlen.',4.30,0,NULL,UTC_TIMESTAMP(6)),
                                                                                                                               ('50000000-0000-0000-0000-000000000002','c0000000-0000-0000-0000-000000000003','e1000000-0000-0000-0000-000000000004','aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1','Zu viele Fehlstunden, wenig Mitarbeit.',4.70,0,NULL,UTC_TIMESTAMP(6)),
                                                                                                                               ('50000000-0000-0000-0000-000000000003','c0000000-0000-0000-0000-000000000011','e1000000-0000-0000-0000-000000000002','aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2','Aufsatz-Qualität sinkt, Abgaben verspätet.',4.10,1,DATE_SUB(UTC_TIMESTAMP(6), INTERVAL 2 DAY),DATE_SUB(UTC_TIMESTAMP(6), INTERVAL 3 DAY));

-- =========================================
-- 10) Notifications
-- type: bei dir in Seed: "Neue Note verfügbar" hatte Type=2
-- =========================================
INSERT INTO notifications (id, user_id, title, message, type, is_read, timestamp) VALUES
                                                                                      ('60000000-0000-0000-0000-000000000001','c0000000-0000-0000-0000-000000000006','Frühwarnung','Du hast eine Frühwarnung in Mathematik erhalten. Bitte melde dich beim Lehrer.',2,0,UTC_TIMESTAMP(6)),
                                                                                      ('60000000-0000-0000-0000-000000000002','c0000000-0000-0000-0000-000000000003','Frühwarnung','Du hast eine Frühwarnung in Programmieren erhalten. Aktueller Schnitt ist kritisch.',2,0,UTC_TIMESTAMP(6)),
                                                                                      ('60000000-0000-0000-0000-000000000003','c0000000-0000-0000-0000-000000000011','Frühwarnung gesendet','Frühwarnung wurde an dich gesendet (Deutsch).',2,1,DATE_SUB(UTC_TIMESTAMP(6), INTERVAL 1 DAY));
