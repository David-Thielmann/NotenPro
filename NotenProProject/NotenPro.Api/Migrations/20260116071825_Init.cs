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
                values: new object[] { "7ea2e317-112a-4673-967c-051882616f4e", new DateTime(2026, 1, 16, 7, 18, 23, 254, DateTimeKind.Utc).AddTicks(2931), "Krems an der Donau", "HTL Krems", "Active", new DateTime(2026, 1, 16, 7, 18, 23, 254, DateTimeKind.Utc).AddTicks(3242) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "external_id", "is_active", "name", "password_hash", "role", "school_id", "updated_at" },
                values: new object[] { "cfcdd489-e0cf-4274-b579-ae1afc964d61", new DateTime(2026, 1, 16, 7, 18, 23, 252, DateTimeKind.Utc).AddTicks(9499), "sysadmin@notenpro.at", "", true, "System Administrator", "$2a$11$cwDTglpdkXqqno1ZmWuG6Ohx7wFEMEpQu3XQat.9HjSR.bgtG3oLa", 3, null, new DateTime(2026, 1, 16, 7, 18, 23, 252, DateTimeKind.Utc).AddTicks(9833) });

            migrationBuilder.InsertData(
                table: "classes",
                columns: new[] { "id", "class_teacher_id", "created_at", "name", "school_id", "updated_at" },
                values: new object[] { "1905611e-f2ae-4b79-bd61-5b7aa6244f64", null, new DateTime(2026, 1, 16, 7, 18, 23, 822, DateTimeKind.Utc).AddTicks(4286), "5BHIT", "7ea2e317-112a-4673-967c-051882616f4e", new DateTime(2026, 1, 16, 7, 18, 23, 822, DateTimeKind.Utc).AddTicks(4286) });

            migrationBuilder.InsertData(
                table: "subjects",
                columns: new[] { "id", "created_at", "description", "is_active", "name", "school_id", "updated_at" },
                values: new object[,]
                {
                    { "35e5d14a-ba00-49fb-ae47-6d752ba3ce88", new DateTime(2026, 1, 16, 7, 18, 23, 822, DateTimeKind.Utc).AddTicks(8233), "English Language", true, "Englisch", "7ea2e317-112a-4673-967c-051882616f4e", new DateTime(2026, 1, 16, 7, 18, 23, 822, DateTimeKind.Utc).AddTicks(8233) },
                    { "9eb7dfc3-93ad-4b71-ad8b-7bbbd9fb624f", new DateTime(2026, 1, 16, 7, 18, 23, 822, DateTimeKind.Utc).AddTicks(8216), "Deutsche Sprache und Literatur", true, "Deutsch", "7ea2e317-112a-4673-967c-051882616f4e", new DateTime(2026, 1, 16, 7, 18, 23, 822, DateTimeKind.Utc).AddTicks(8222) },
                    { "df3fbd45-39ed-4f13-b194-c64e2f55e291", new DateTime(2026, 1, 16, 7, 18, 23, 822, DateTimeKind.Utc).AddTicks(7633), "Angewandte Mathematik", true, "Mathematik", "7ea2e317-112a-4673-967c-051882616f4e", new DateTime(2026, 1, 16, 7, 18, 23, 822, DateTimeKind.Utc).AddTicks(7929) },
                    { "ec8dec77-ecbd-4031-a601-8dd72428e450", new DateTime(2026, 1, 16, 7, 18, 23, 822, DateTimeKind.Utc).AddTicks(8264), "Software Engineering", true, "Programmieren", "7ea2e317-112a-4673-967c-051882616f4e", new DateTime(2026, 1, 16, 7, 18, 23, 822, DateTimeKind.Utc).AddTicks(8265) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "external_id", "is_active", "name", "password_hash", "role", "school_id", "updated_at" },
                values: new object[,]
                {
                    { "72401289-ab71-4d04-bd16-e374bb7fc257", new DateTime(2026, 1, 16, 7, 18, 23, 821, DateTimeKind.Utc).AddTicks(9198), "maria.schmidt@htl-krems.ac.at", "", true, "Prof. Maria Schmidt", "$2a$11$9lxohmpui3k7iidhkBtAOe0BLnpabBWBdR.wj9yK/ERXnPdTpw3LK", 1, "7ea2e317-112a-4673-967c-051882616f4e", new DateTime(2026, 1, 16, 7, 18, 23, 821, DateTimeKind.Utc).AddTicks(9202) },
                    { "7c11c4de-8754-4864-a1cc-d69be562fa72", new DateTime(2026, 1, 16, 7, 18, 23, 649, DateTimeKind.Utc).AddTicks(5157), "admin@htl-krems.ac.at", "", true, "HTL Admin", "$2a$11$VskBSIPXQ/2NrJUHdq4ujem620MWYwoLh2U4VyHkgj1fMgzCS4m7.", 2, "7ea2e317-112a-4673-967c-051882616f4e", new DateTime(2026, 1, 16, 7, 18, 23, 649, DateTimeKind.Utc).AddTicks(5161) },
                    { "faf52356-a0cf-4777-a518-ddefe7e20ffe", new DateTime(2026, 1, 16, 7, 18, 23, 997, DateTimeKind.Utc).AddTicks(1182), "max.mustermann@students.htl-krems.ac.at", "", true, "Max Mustermann", "$2a$11$5bkWWWhflciyB5Ta90eLyedISlwTZtTa8FQWLRSlKC3zhii1UbqX.", 0, "7ea2e317-112a-4673-967c-051882616f4e", new DateTime(2026, 1, 16, 7, 18, 23, 997, DateTimeKind.Utc).AddTicks(1186) }
                });

            migrationBuilder.InsertData(
                table: "classes",
                columns: new[] { "id", "class_teacher_id", "created_at", "name", "school_id", "updated_at" },
                values: new object[] { "f064126f-70fb-413c-9609-daf1e25979b9", "72401289-ab71-4d04-bd16-e374bb7fc257", new DateTime(2026, 1, 16, 7, 18, 23, 822, DateTimeKind.Utc).AddTicks(3312), "5AHIT", "7ea2e317-112a-4673-967c-051882616f4e", new DateTime(2026, 1, 16, 7, 18, 23, 822, DateTimeKind.Utc).AddTicks(3723) });

            migrationBuilder.InsertData(
                table: "notifications",
                columns: new[] { "id", "is_read", "message", "timestamp", "title", "type", "user_id" },
                values: new object[] { "008874fa-ae31-4822-9e70-2a10ae71ecc6", false, "Deine Note für 'Algebra Test 1' wurde eingetragen: 2.00 (Gut)", new DateTime(2026, 1, 16, 7, 18, 23, 998, DateTimeKind.Utc).AddTicks(8784), "Neue Note verfügbar", 2, "faf52356-a0cf-4777-a518-ddefe7e20ffe" });

            migrationBuilder.InsertData(
                table: "teacher_subjects",
                columns: new[] { "id", "assigned_at", "subject_id", "teacher_id" },
                values: new object[,]
                {
                    { "2f50b156-7bd4-4947-87f2-b019bb7aece9", new DateTime(2026, 1, 16, 7, 18, 23, 823, DateTimeKind.Utc).AddTicks(397), "df3fbd45-39ed-4f13-b194-c64e2f55e291", "72401289-ab71-4d04-bd16-e374bb7fc257" },
                    { "c02492f4-7ca5-4d2b-93da-437877196886", new DateTime(2026, 1, 16, 7, 18, 23, 823, DateTimeKind.Utc).AddTicks(1049), "ec8dec77-ecbd-4031-a601-8dd72428e450", "72401289-ab71-4d04-bd16-e374bb7fc257" }
                });

            migrationBuilder.InsertData(
                table: "student_classes",
                columns: new[] { "id", "class_id", "enrolled_at", "student_id" },
                values: new object[] { "e96ffab7-fab4-479a-8220-a3450621efc4", "f064126f-70fb-413c-9609-daf1e25979b9", new DateTime(2026, 1, 16, 7, 18, 23, 997, DateTimeKind.Utc).AddTicks(4452), "faf52356-a0cf-4777-a518-ddefe7e20ffe" });

            migrationBuilder.InsertData(
                table: "tests",
                columns: new[] { "id", "class_id", "created_at", "date", "description", "max_points", "name", "subject_id", "teacher_id", "type", "updated_at" },
                values: new object[] { "3cbcd1c7-884b-4fa3-b3a2-2d7a10fe3761", "f064126f-70fb-413c-9609-daf1e25979b9", new DateTime(2026, 1, 16, 7, 18, 23, 998, DateTimeKind.Utc).AddTicks(67), new DateTime(2026, 1, 9, 7, 18, 23, 997, DateTimeKind.Utc).AddTicks(8723), "Lineare Gleichungen und Funktionen", 100, "Algebra Test 1", "df3fbd45-39ed-4f13-b194-c64e2f55e291", "72401289-ab71-4d04-bd16-e374bb7fc257", 0, new DateTime(2026, 1, 16, 7, 18, 23, 998, DateTimeKind.Utc).AddTicks(519) });

            migrationBuilder.InsertData(
                table: "grades",
                columns: new[] { "id", "comment", "created_at", "grade_value", "max_points", "points", "status", "student_id", "test_id", "updated_at" },
                values: new object[] { "1fd5e77c-033e-48bf-8da5-fa923a04483a", "Sehr gute Leistung!", new DateTime(2026, 1, 16, 7, 18, 23, 998, DateTimeKind.Utc).AddTicks(5150), 2.00m, 100, 82, 0, "faf52356-a0cf-4777-a518-ddefe7e20ffe", "3cbcd1c7-884b-4fa3-b3a2-2d7a10fe3761", new DateTime(2026, 1, 16, 7, 18, 23, 998, DateTimeKind.Utc).AddTicks(5425) });

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
