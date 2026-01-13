using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotenPro.Api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "schools",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    location = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schools", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    school_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.id);
                    table.ForeignKey(
                        name: "FK_subjects_schools_school_id",
                        column: x => x.school_id,
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<int>(type: "int", nullable: false),
                    school_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    external_id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_schools_school_id",
                        column: x => x.school_id,
                        principalTable: "schools",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    school_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    class_teacher_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes", x => x.id);
                    table.ForeignKey(
                        name: "FK_classes_schools_school_id",
                        column: x => x.school_id,
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_classes_users_class_teacher_id",
                        column: x => x.class_teacher_id,
                        principalTable: "users",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "early_warnings",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    student_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subject_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    teacher_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reason = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    current_average = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    is_sent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    sent_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_early_warnings", x => x.id);
                    table.ForeignKey(
                        name: "FK_early_warnings_subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_early_warnings_users_student_id",
                        column: x => x.student_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_early_warnings_users_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    message = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<int>(type: "int", nullable: false),
                    is_read = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_notifications_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teacher_subjects",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    teacher_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subject_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assigned_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_subjects", x => x.id);
                    table.ForeignKey(
                        name: "FK_teacher_subjects_subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_teacher_subjects_users_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "student_classes",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    student_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    class_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    enrolled_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_classes", x => x.id);
                    table.ForeignKey(
                        name: "FK_student_classes_classes_class_id",
                        column: x => x.class_id,
                        principalTable: "classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_student_classes_users_student_id",
                        column: x => x.student_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tests",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subject_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    class_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    teacher_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    max_points = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tests", x => x.id);
                    table.ForeignKey(
                        name: "FK_tests_classes_class_id",
                        column: x => x.class_id,
                        principalTable: "classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tests_subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tests_users_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    student_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    test_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    grade_value = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: true),
                    points = table.Column<int>(type: "int", nullable: true),
                    max_points = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grades", x => x.id);
                    table.ForeignKey(
                        name: "FK_grades_tests_test_id",
                        column: x => x.test_id,
                        principalTable: "tests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grades_users_student_id",
                        column: x => x.student_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "schools",
                columns: new[] { "id", "created_at", "location", "name", "status", "updated_at" },
                values: new object[] { "db717465-da54-417d-bcf0-9867870774ba", new DateTime(2026, 1, 13, 9, 27, 31, 828, DateTimeKind.Utc).AddTicks(1758), "Krems an der Donau", "HTL Krems", "Active", new DateTime(2026, 1, 13, 9, 27, 31, 828, DateTimeKind.Utc).AddTicks(2191) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "external_id", "is_active", "name", "password_hash", "role", "school_id", "updated_at" },
                values: new object[] { "aa63eeaf-7628-44d5-8c41-0868d195fef6", new DateTime(2026, 1, 13, 9, 27, 31, 826, DateTimeKind.Utc).AddTicks(4489), "sysadmin@notenpro.at", "", true, "System Administrator", "$2a$11$oXWxpJJMr9h8zQFjqtG.Vu7MTWDnr0gPiPe79u.2d3HqR.AjmSN.u", 3, null, new DateTime(2026, 1, 13, 9, 27, 31, 826, DateTimeKind.Utc).AddTicks(5087) });

            migrationBuilder.InsertData(
                table: "classes",
                columns: new[] { "id", "class_teacher_id", "created_at", "name", "school_id", "updated_at" },
                values: new object[] { "5019655e-4dbc-4f5e-9e0a-85eb10161f07", null, new DateTime(2026, 1, 13, 9, 27, 32, 312, DateTimeKind.Utc).AddTicks(7477), "5BHIT", "db717465-da54-417d-bcf0-9867870774ba", new DateTime(2026, 1, 13, 9, 27, 32, 312, DateTimeKind.Utc).AddTicks(7478) });

            migrationBuilder.InsertData(
                table: "subjects",
                columns: new[] { "id", "created_at", "description", "is_active", "name", "school_id", "updated_at" },
                values: new object[,]
                {
                    { "74ebf9f0-b178-4b80-a76e-56f744af573c", new DateTime(2026, 1, 13, 9, 27, 32, 313, DateTimeKind.Utc).AddTicks(6147), "English Language", true, "Englisch", "db717465-da54-417d-bcf0-9867870774ba", new DateTime(2026, 1, 13, 9, 27, 32, 313, DateTimeKind.Utc).AddTicks(6148) },
                    { "955d437b-25f0-41cc-a174-cb2c1331c3b3", new DateTime(2026, 1, 13, 9, 27, 32, 313, DateTimeKind.Utc).AddTicks(6120), "Deutsche Sprache und Literatur", true, "Deutsch", "db717465-da54-417d-bcf0-9867870774ba", new DateTime(2026, 1, 13, 9, 27, 32, 313, DateTimeKind.Utc).AddTicks(6133) },
                    { "9d9a2c26-0f79-490e-8db9-f1d750c05ece", new DateTime(2026, 1, 13, 9, 27, 32, 313, DateTimeKind.Utc).AddTicks(6164), "Software Engineering", true, "Programmieren", "db717465-da54-417d-bcf0-9867870774ba", new DateTime(2026, 1, 13, 9, 27, 32, 313, DateTimeKind.Utc).AddTicks(6165) },
                    { "d357c9d0-5296-4d0d-af55-84e9da2f6518", new DateTime(2026, 1, 13, 9, 27, 32, 313, DateTimeKind.Utc).AddTicks(4708), "Angewandte Mathematik", true, "Mathematik", "db717465-da54-417d-bcf0-9867870774ba", new DateTime(2026, 1, 13, 9, 27, 32, 313, DateTimeKind.Utc).AddTicks(5348) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "external_id", "is_active", "name", "password_hash", "role", "school_id", "updated_at" },
                values: new object[,]
                {
                    { "1a57c275-b107-4442-a8ae-513faa74143d", new DateTime(2026, 1, 13, 9, 27, 32, 75, DateTimeKind.Utc).AddTicks(2406), "admin@htl-krems.ac.at", "", true, "HTL Admin", "$2a$11$qoxsf5L05CckULJjWJwMVu4ymj/xgerlus4BZJwmoTW9MVLdLOIwO", 2, "db717465-da54-417d-bcf0-9867870774ba", new DateTime(2026, 1, 13, 9, 27, 32, 75, DateTimeKind.Utc).AddTicks(2412) },
                    { "6db9373f-5d9f-4186-82dd-26282371f2b1", new DateTime(2026, 1, 13, 9, 27, 32, 548, DateTimeKind.Utc).AddTicks(2769), "max.mustermann@students.htl-krems.ac.at", "", true, "Max Mustermann", "$2a$11$UbKyhg2CwTJZye21z.7OmO54XC/x06X5N7VELvAD5MAtV9AfGv4TW", 0, "db717465-da54-417d-bcf0-9867870774ba", new DateTime(2026, 1, 13, 9, 27, 32, 548, DateTimeKind.Utc).AddTicks(2772) },
                    { "ed4c5c9f-c09b-4424-987f-8782f828c07c", new DateTime(2026, 1, 13, 9, 27, 32, 311, DateTimeKind.Utc).AddTicks(7639), "maria.schmidt@htl-krems.ac.at", "", true, "Prof. Maria Schmidt", "$2a$11$/Ky6muhpQPW600xVJCX9wumKKs2SNSFmGFxCRB48FyRlWH4QJ2IoW", 1, "db717465-da54-417d-bcf0-9867870774ba", new DateTime(2026, 1, 13, 9, 27, 32, 311, DateTimeKind.Utc).AddTicks(7647) }
                });

            migrationBuilder.InsertData(
                table: "classes",
                columns: new[] { "id", "class_teacher_id", "created_at", "name", "school_id", "updated_at" },
                values: new object[] { "b47ddd1b-48e3-45b0-ad11-3a817402e00c", "ed4c5c9f-c09b-4424-987f-8782f828c07c", new DateTime(2026, 1, 13, 9, 27, 32, 312, DateTimeKind.Utc).AddTicks(6065), "5AHIT", "db717465-da54-417d-bcf0-9867870774ba", new DateTime(2026, 1, 13, 9, 27, 32, 312, DateTimeKind.Utc).AddTicks(6789) });

            migrationBuilder.InsertData(
                table: "notifications",
                columns: new[] { "id", "is_read", "message", "timestamp", "title", "type", "user_id" },
                values: new object[] { "1fd7a96b-d211-46a9-ba6c-ef46fa3b1386", false, "Deine Note für 'Algebra Test 1' wurde eingetragen: 2.00 (Gut)", new DateTime(2026, 1, 13, 9, 27, 32, 550, DateTimeKind.Utc).AddTicks(5092), "Neue Note verfügbar", 2, "6db9373f-5d9f-4186-82dd-26282371f2b1" });

            migrationBuilder.InsertData(
                table: "teacher_subjects",
                columns: new[] { "id", "assigned_at", "subject_id", "teacher_id" },
                values: new object[,]
                {
                    { "5872c160-baa8-4532-a1a7-54038536050a", new DateTime(2026, 1, 13, 9, 27, 32, 314, DateTimeKind.Utc).AddTicks(998), "d357c9d0-5296-4d0d-af55-84e9da2f6518", "ed4c5c9f-c09b-4424-987f-8782f828c07c" },
                    { "82a8cb90-676d-43b2-bece-0e7fb237e8db", new DateTime(2026, 1, 13, 9, 27, 32, 314, DateTimeKind.Utc).AddTicks(1676), "9d9a2c26-0f79-490e-8db9-f1d750c05ece", "ed4c5c9f-c09b-4424-987f-8782f828c07c" }
                });

            migrationBuilder.InsertData(
                table: "student_classes",
                columns: new[] { "id", "class_id", "enrolled_at", "student_id" },
                values: new object[] { "8791934c-8aed-4206-8dc7-d03ccf83248f", "b47ddd1b-48e3-45b0-ad11-3a817402e00c", new DateTime(2026, 1, 13, 9, 27, 32, 548, DateTimeKind.Utc).AddTicks(7194), "6db9373f-5d9f-4186-82dd-26282371f2b1" });

            migrationBuilder.InsertData(
                table: "tests",
                columns: new[] { "id", "class_id", "created_at", "date", "description", "max_points", "name", "subject_id", "teacher_id", "type", "updated_at" },
                values: new object[] { "40cfe18f-35a2-425e-94b1-09c2a639c19d", "b47ddd1b-48e3-45b0-ad11-3a817402e00c", new DateTime(2026, 1, 13, 9, 27, 32, 549, DateTimeKind.Utc).AddTicks(4178), new DateTime(2026, 1, 6, 9, 27, 32, 549, DateTimeKind.Utc).AddTicks(2545), "Lineare Gleichungen und Funktionen", 100, "Algebra Test 1", "d357c9d0-5296-4d0d-af55-84e9da2f6518", "ed4c5c9f-c09b-4424-987f-8782f828c07c", 0, new DateTime(2026, 1, 13, 9, 27, 32, 549, DateTimeKind.Utc).AddTicks(4555) });

            migrationBuilder.InsertData(
                table: "grades",
                columns: new[] { "id", "comment", "created_at", "grade_value", "max_points", "points", "status", "student_id", "test_id", "updated_at" },
                values: new object[] { "99e19ab9-8774-4550-9564-cf2c0e49f7ea", "Sehr gute Leistung!", new DateTime(2026, 1, 13, 9, 27, 32, 550, DateTimeKind.Utc).AddTicks(625), 2.00m, 100, 82, 0, "6db9373f-5d9f-4186-82dd-26282371f2b1", "40cfe18f-35a2-425e-94b1-09c2a639c19d", new DateTime(2026, 1, 13, 9, 27, 32, 550, DateTimeKind.Utc).AddTicks(983) });

            migrationBuilder.CreateIndex(
                name: "IX_classes_class_teacher_id",
                table: "classes",
                column: "class_teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_classes_school_id_name",
                table: "classes",
                columns: new[] { "school_id", "name" });

            migrationBuilder.CreateIndex(
                name: "IX_early_warnings_student_id_subject_id_created_at",
                table: "early_warnings",
                columns: new[] { "student_id", "subject_id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "IX_early_warnings_subject_id",
                table: "early_warnings",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_early_warnings_teacher_id",
                table: "early_warnings",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_grades_student_id_test_id",
                table: "grades",
                columns: new[] { "student_id", "test_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_grades_test_id",
                table: "grades",
                column: "test_id");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_user_id_timestamp",
                table: "notifications",
                columns: new[] { "user_id", "timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_schools_name",
                table: "schools",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_student_classes_class_id",
                table: "student_classes",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_classes_student_id_class_id",
                table: "student_classes",
                columns: new[] { "student_id", "class_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subjects_school_id_name",
                table: "subjects",
                columns: new[] { "school_id", "name" });

            migrationBuilder.CreateIndex(
                name: "IX_teacher_subjects_subject_id",
                table: "teacher_subjects",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_subjects_teacher_id_subject_id",
                table: "teacher_subjects",
                columns: new[] { "teacher_id", "subject_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tests_class_id_date",
                table: "tests",
                columns: new[] { "class_id", "date" });

            migrationBuilder.CreateIndex(
                name: "IX_tests_subject_id",
                table: "tests",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_tests_teacher_id",
                table: "tests",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_school_id",
                table: "users",
                column: "school_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "early_warnings");

            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "student_classes");

            migrationBuilder.DropTable(
                name: "teacher_subjects");

            migrationBuilder.DropTable(
                name: "tests");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "schools");
        }
    }
}
