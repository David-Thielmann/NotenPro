using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotenPro.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMySql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "classes",
                keyColumn: "id",
                keyValue: "b107a15b-ad5e-43cb-843f-d5ebedb48bd0");

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: "e502cf32-72f6-43f1-9a95-3852798ddf1a");

            migrationBuilder.DeleteData(
                table: "notifications",
                keyColumn: "id",
                keyValue: "bc4b39cb-33a3-44ad-8351-ea5ba0c169a7");

            migrationBuilder.DeleteData(
                table: "student_classes",
                keyColumn: "id",
                keyValue: "5991c8ed-8000-4be9-a691-ce8f2f2aa0a4");

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: "21bdc7e3-d9a8-4da0-90dd-ba0fe964ab02");

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: "fa849cb8-770b-4944-b7a0-683b1dc2c680");

            migrationBuilder.DeleteData(
                table: "teacher_subjects",
                keyColumn: "id",
                keyValue: "91c77a66-2582-425c-9070-4f4c4869bdc9");

            migrationBuilder.DeleteData(
                table: "teacher_subjects",
                keyColumn: "id",
                keyValue: "a69cfaf5-c828-47d6-ae0a-728c19f2670d");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "01ff43a6-7484-4c01-8a98-a867baae0c51");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "2984991f-7da0-445f-bdfb-b897226c01a6");

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: "390c0f0b-c57f-4cba-8229-82e0d3c670f8");

            migrationBuilder.DeleteData(
                table: "tests",
                keyColumn: "id",
                keyValue: "d82d4ae1-4b46-4a7b-b6f4-4f3e1af74e64");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "45ea76af-3cb3-4a3f-a1ae-69783105c971");

            migrationBuilder.DeleteData(
                table: "classes",
                keyColumn: "id",
                keyValue: "4ed42a29-bb73-4d1a-8a34-f0463a54098c");

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: "95733880-5dd3-41bd-92d3-b6690ab2edcf");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "87a934f4-1428-47aa-9368-bdbade74b9ec");

            migrationBuilder.DeleteData(
                table: "schools",
                keyColumn: "id",
                keyValue: "b47f684d-6000-42cf-ad82-b5f8d36fde9e");

            migrationBuilder.InsertData(
                table: "schools",
                columns: new[] { "id", "created_at", "location", "name", "status", "updated_at" },
                values: new object[] { "20fe3462-07c4-426d-91e0-033fd17d820a", new DateTime(2025, 12, 11, 9, 11, 44, 427, DateTimeKind.Utc).AddTicks(6150), "Krems an der Donau", "HTL Krems", "Active", new DateTime(2025, 12, 11, 9, 11, 44, 427, DateTimeKind.Utc).AddTicks(6389) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "is_active", "name", "password_hash", "role", "school_id", "updated_at" },
                values: new object[] { "aaf3d1f8-0bc3-4197-bd48-1365f4d66fb2", new DateTime(2025, 12, 11, 9, 11, 44, 426, DateTimeKind.Utc).AddTicks(6068), "sysadmin@notenpro.at", true, "System Administrator", "$2a$11$b5h2.aUW5UItWKz5Dyf71e3nZtHma4Dg4Lg5Hsvf/qa8FDisNkfSa", 3, null, new DateTime(2025, 12, 11, 9, 11, 44, 426, DateTimeKind.Utc).AddTicks(6335) });

            migrationBuilder.InsertData(
                table: "classes",
                columns: new[] { "id", "class_teacher_id", "created_at", "name", "school_id", "updated_at" },
                values: new object[] { "7fee9762-3b89-4bd9-9f7b-250b493a5644", null, new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(2090), "5BHIT", "20fe3462-07c4-426d-91e0-033fd17d820a", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(2091) });

            migrationBuilder.InsertData(
                table: "subjects",
                columns: new[] { "id", "created_at", "description", "is_active", "name", "school_id", "updated_at" },
                values: new object[,]
                {
                    { "675d7ccb-746e-4c55-9288-ccb1d14858d5", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(5625), "English Language", true, "Englisch", "20fe3462-07c4-426d-91e0-033fd17d820a", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(5625) },
                    { "6c7dcb42-a1b2-4607-9e2d-95be7498b92c", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(4895), "Angewandte Mathematik", true, "Mathematik", "20fe3462-07c4-426d-91e0-033fd17d820a", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(5123) },
                    { "94614d7e-c9d5-41da-b217-64bc4795d75f", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(5632), "Software Engineering", true, "Programmieren", "20fe3462-07c4-426d-91e0-033fd17d820a", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(5632) },
                    { "acd30c33-950a-48ba-8887-c41e4aeaf507", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(5617), "Deutsche Sprache und Literatur", true, "Deutsch", "20fe3462-07c4-426d-91e0-033fd17d820a", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(5622) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "is_active", "name", "password_hash", "role", "school_id", "updated_at" },
                values: new object[,]
                {
                    { "a6aaa29b-1db6-467a-ab28-4122bafc1061", new DateTime(2025, 12, 11, 9, 11, 44, 866, DateTimeKind.Utc).AddTicks(91), "max.mustermann@students.htl-krems.ac.at", true, "Max Mustermann", "$2a$11$SuDsEK4tYx2klDAjp.89culDtfaLhEtrJ9xg7abMs.njyDjcmIgCC", 0, "20fe3462-07c4-426d-91e0-033fd17d820a", new DateTime(2025, 12, 11, 9, 11, 44, 866, DateTimeKind.Utc).AddTicks(94) },
                    { "abbd0f05-349f-4e22-8427-dc126fcb6284", new DateTime(2025, 12, 11, 9, 11, 44, 710, DateTimeKind.Utc).AddTicks(7506), "maria.schmidt@htl-krems.ac.at", true, "Prof. Maria Schmidt", "$2a$11$ReT4loFQTLWa1BGtioZDdey3tmo8EhSiJJwfcieGm8m7oXIt1AG.W", 1, "20fe3462-07c4-426d-91e0-033fd17d820a", new DateTime(2025, 12, 11, 9, 11, 44, 710, DateTimeKind.Utc).AddTicks(7510) },
                    { "b90435c6-022f-4a1e-a06f-540b76f71aa9", new DateTime(2025, 12, 11, 9, 11, 44, 566, DateTimeKind.Utc).AddTicks(8099), "admin@htl-krems.ac.at", true, "HTL Admin", "$2a$11$DnIOa0lBL4JwPA0f3tnzAeyRTq0U32nqacqo51GmooU9OmGj7X88O", 2, "20fe3462-07c4-426d-91e0-033fd17d820a", new DateTime(2025, 12, 11, 9, 11, 44, 566, DateTimeKind.Utc).AddTicks(8103) }
                });

            migrationBuilder.InsertData(
                table: "classes",
                columns: new[] { "id", "class_teacher_id", "created_at", "name", "school_id", "updated_at" },
                values: new object[] { "33a31a22-4070-4097-9c09-a9996c104509", "abbd0f05-349f-4e22-8427-dc126fcb6284", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(1573), "5AHIT", "20fe3462-07c4-426d-91e0-033fd17d820a", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(1833) });

            migrationBuilder.InsertData(
                table: "notifications",
                columns: new[] { "id", "is_read", "message", "timestamp", "title", "type", "user_id" },
                values: new object[] { "6f3d8d40-4a04-4223-8a5e-73b01a3c936d", false, "Deine Note für 'Algebra Test 1' wurde eingetragen: 2.00 (Gut)", new DateTime(2025, 12, 11, 9, 11, 44, 867, DateTimeKind.Utc).AddTicks(8727), "Neue Note verfügbar", 2, "a6aaa29b-1db6-467a-ab28-4122bafc1061" });

            migrationBuilder.InsertData(
                table: "teacher_subjects",
                columns: new[] { "id", "assigned_at", "subject_id", "teacher_id" },
                values: new object[,]
                {
                    { "93f02d45-0845-4815-a2f8-84dd2a84b585", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(7475), "94614d7e-c9d5-41da-b217-64bc4795d75f", "abbd0f05-349f-4e22-8427-dc126fcb6284" },
                    { "9caaa60d-0e7b-4efd-bb0d-20f099271307", new DateTime(2025, 12, 11, 9, 11, 44, 711, DateTimeKind.Utc).AddTicks(7232), "6c7dcb42-a1b2-4607-9e2d-95be7498b92c", "abbd0f05-349f-4e22-8427-dc126fcb6284" }
                });

            migrationBuilder.InsertData(
                table: "student_classes",
                columns: new[] { "id", "class_id", "enrolled_at", "student_id" },
                values: new object[] { "a6c342e3-c078-4d0e-9307-3ce3a08cb8e0", "33a31a22-4070-4097-9c09-a9996c104509", new DateTime(2025, 12, 11, 9, 11, 44, 866, DateTimeKind.Utc).AddTicks(3519), "a6aaa29b-1db6-467a-ab28-4122bafc1061" });

            migrationBuilder.InsertData(
                table: "tests",
                columns: new[] { "id", "class_id", "created_at", "date", "description", "max_points", "name", "subject_id", "teacher_id", "type", "updated_at" },
                values: new object[] { "fd537b2a-6625-4564-a75c-c3d0ca58c922", "33a31a22-4070-4097-9c09-a9996c104509", new DateTime(2025, 12, 11, 9, 11, 44, 867, DateTimeKind.Utc).AddTicks(698), new DateTime(2025, 12, 4, 9, 11, 44, 866, DateTimeKind.Utc).AddTicks(8858), "Lineare Gleichungen und Funktionen", 100, "Algebra Test 1", "6c7dcb42-a1b2-4607-9e2d-95be7498b92c", "abbd0f05-349f-4e22-8427-dc126fcb6284", 0, new DateTime(2025, 12, 11, 9, 11, 44, 867, DateTimeKind.Utc).AddTicks(961) });

            migrationBuilder.InsertData(
                table: "grades",
                columns: new[] { "id", "comment", "created_at", "grade_value", "max_points", "points", "status", "student_id", "test_id", "updated_at" },
                values: new object[] { "03b24fcc-a1f2-4290-9982-b9b83c42ff1c", "Sehr gute Leistung!", new DateTime(2025, 12, 11, 9, 11, 44, 867, DateTimeKind.Utc).AddTicks(5507), 2.00m, 100, 82, 0, "a6aaa29b-1db6-467a-ab28-4122bafc1061", "fd537b2a-6625-4564-a75c-c3d0ca58c922", new DateTime(2025, 12, 11, 9, 11, 44, 867, DateTimeKind.Utc).AddTicks(5738) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "classes",
                keyColumn: "id",
                keyValue: "7fee9762-3b89-4bd9-9f7b-250b493a5644");

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: "03b24fcc-a1f2-4290-9982-b9b83c42ff1c");

            migrationBuilder.DeleteData(
                table: "notifications",
                keyColumn: "id",
                keyValue: "6f3d8d40-4a04-4223-8a5e-73b01a3c936d");

            migrationBuilder.DeleteData(
                table: "student_classes",
                keyColumn: "id",
                keyValue: "a6c342e3-c078-4d0e-9307-3ce3a08cb8e0");

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: "675d7ccb-746e-4c55-9288-ccb1d14858d5");

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: "acd30c33-950a-48ba-8887-c41e4aeaf507");

            migrationBuilder.DeleteData(
                table: "teacher_subjects",
                keyColumn: "id",
                keyValue: "93f02d45-0845-4815-a2f8-84dd2a84b585");

            migrationBuilder.DeleteData(
                table: "teacher_subjects",
                keyColumn: "id",
                keyValue: "9caaa60d-0e7b-4efd-bb0d-20f099271307");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "aaf3d1f8-0bc3-4197-bd48-1365f4d66fb2");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "b90435c6-022f-4a1e-a06f-540b76f71aa9");

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: "94614d7e-c9d5-41da-b217-64bc4795d75f");

            migrationBuilder.DeleteData(
                table: "tests",
                keyColumn: "id",
                keyValue: "fd537b2a-6625-4564-a75c-c3d0ca58c922");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "a6aaa29b-1db6-467a-ab28-4122bafc1061");

            migrationBuilder.DeleteData(
                table: "classes",
                keyColumn: "id",
                keyValue: "33a31a22-4070-4097-9c09-a9996c104509");

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: "6c7dcb42-a1b2-4607-9e2d-95be7498b92c");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "abbd0f05-349f-4e22-8427-dc126fcb6284");

            migrationBuilder.DeleteData(
                table: "schools",
                keyColumn: "id",
                keyValue: "20fe3462-07c4-426d-91e0-033fd17d820a");

            migrationBuilder.InsertData(
                table: "schools",
                columns: new[] { "id", "created_at", "location", "name", "status", "updated_at" },
                values: new object[] { "b47f684d-6000-42cf-ad82-b5f8d36fde9e", new DateTime(2025, 12, 11, 9, 11, 25, 303, DateTimeKind.Utc).AddTicks(4788), "Krems an der Donau", "HTL Krems", "Active", new DateTime(2025, 12, 11, 9, 11, 25, 303, DateTimeKind.Utc).AddTicks(5036) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "is_active", "name", "password_hash", "role", "school_id", "updated_at" },
                values: new object[] { "2984991f-7da0-445f-bdfb-b897226c01a6", new DateTime(2025, 12, 11, 9, 11, 25, 302, DateTimeKind.Utc).AddTicks(4502), "sysadmin@notenpro.at", true, "System Administrator", "$2a$11$ukrZFKs2IgrERRgyXArgp.yqEd0y5uilTzhpF24SpKe.vM09.B5Oy", 3, null, new DateTime(2025, 12, 11, 9, 11, 25, 302, DateTimeKind.Utc).AddTicks(4780) });

            migrationBuilder.InsertData(
                table: "classes",
                columns: new[] { "id", "class_teacher_id", "created_at", "name", "school_id", "updated_at" },
                values: new object[] { "b107a15b-ad5e-43cb-843f-d5ebedb48bd0", null, new DateTime(2025, 12, 11, 9, 11, 25, 575, DateTimeKind.Utc).AddTicks(8535), "5BHIT", "b47f684d-6000-42cf-ad82-b5f8d36fde9e", new DateTime(2025, 12, 11, 9, 11, 25, 575, DateTimeKind.Utc).AddTicks(8535) });

            migrationBuilder.InsertData(
                table: "subjects",
                columns: new[] { "id", "created_at", "description", "is_active", "name", "school_id", "updated_at" },
                values: new object[,]
                {
                    { "21bdc7e3-d9a8-4da0-90dd-ba0fe964ab02", new DateTime(2025, 12, 11, 9, 11, 25, 576, DateTimeKind.Utc).AddTicks(2037), "Deutsche Sprache und Literatur", true, "Deutsch", "b47f684d-6000-42cf-ad82-b5f8d36fde9e", new DateTime(2025, 12, 11, 9, 11, 25, 576, DateTimeKind.Utc).AddTicks(2043) },
                    { "390c0f0b-c57f-4cba-8229-82e0d3c670f8", new DateTime(2025, 12, 11, 9, 11, 25, 576, DateTimeKind.Utc).AddTicks(2057), "Software Engineering", true, "Programmieren", "b47f684d-6000-42cf-ad82-b5f8d36fde9e", new DateTime(2025, 12, 11, 9, 11, 25, 576, DateTimeKind.Utc).AddTicks(2057) },
                    { "95733880-5dd3-41bd-92d3-b6690ab2edcf", new DateTime(2025, 12, 11, 9, 11, 25, 576, DateTimeKind.Utc).AddTicks(1591), "Angewandte Mathematik", true, "Mathematik", "b47f684d-6000-42cf-ad82-b5f8d36fde9e", new DateTime(2025, 12, 11, 9, 11, 25, 576, DateTimeKind.Utc).AddTicks(1817) },
                    { "fa849cb8-770b-4944-b7a0-683b1dc2c680", new DateTime(2025, 12, 11, 9, 11, 25, 576, DateTimeKind.Utc).AddTicks(2047), "English Language", true, "Englisch", "b47f684d-6000-42cf-ad82-b5f8d36fde9e", new DateTime(2025, 12, 11, 9, 11, 25, 576, DateTimeKind.Utc).AddTicks(2047) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "is_active", "name", "password_hash", "role", "school_id", "updated_at" },
                values: new object[,]
                {
                    { "01ff43a6-7484-4c01-8a98-a867baae0c51", new DateTime(2025, 12, 11, 9, 11, 25, 439, DateTimeKind.Utc).AddTicks(6709), "admin@htl-krems.ac.at", true, "HTL Admin", "$2a$11$nuXY69n/os/HJWum6Kbo6.Vm0eKbgPQpN60QLvwepKiFLhidK3r1S", 2, "b47f684d-6000-42cf-ad82-b5f8d36fde9e", new DateTime(2025, 12, 11, 9, 11, 25, 439, DateTimeKind.Utc).AddTicks(6713) },
                    { "45ea76af-3cb3-4a3f-a1ae-69783105c971", new DateTime(2025, 12, 11, 9, 11, 25, 710, DateTimeKind.Utc).AddTicks(1093), "max.mustermann@students.htl-krems.ac.at", true, "Max Mustermann", "$2a$11$SGwN.k5qbzPv3/9HaFEe3O9mgJnz5OZzdxez3/TVzJBRQPEw4aD7K", 0, "b47f684d-6000-42cf-ad82-b5f8d36fde9e", new DateTime(2025, 12, 11, 9, 11, 25, 710, DateTimeKind.Utc).AddTicks(1096) },
                    { "87a934f4-1428-47aa-9368-bdbade74b9ec", new DateTime(2025, 12, 11, 9, 11, 25, 575, DateTimeKind.Utc).AddTicks(3747), "maria.schmidt@htl-krems.ac.at", true, "Prof. Maria Schmidt", "$2a$11$.zs56wZ0vf4StP5.aBd.hOquGA2IfYSXMgrx42/fWwkvw/uWeMS4.", 1, "b47f684d-6000-42cf-ad82-b5f8d36fde9e", new DateTime(2025, 12, 11, 9, 11, 25, 575, DateTimeKind.Utc).AddTicks(3755) }
                });

            migrationBuilder.InsertData(
                table: "classes",
                columns: new[] { "id", "class_teacher_id", "created_at", "name", "school_id", "updated_at" },
                values: new object[] { "4ed42a29-bb73-4d1a-8a34-f0463a54098c", "87a934f4-1428-47aa-9368-bdbade74b9ec", new DateTime(2025, 12, 11, 9, 11, 25, 575, DateTimeKind.Utc).AddTicks(7924), "5AHIT", "b47f684d-6000-42cf-ad82-b5f8d36fde9e", new DateTime(2025, 12, 11, 9, 11, 25, 575, DateTimeKind.Utc).AddTicks(8231) });

            migrationBuilder.InsertData(
                table: "notifications",
                columns: new[] { "id", "is_read", "message", "timestamp", "title", "type", "user_id" },
                values: new object[] { "bc4b39cb-33a3-44ad-8351-ea5ba0c169a7", false, "Deine Note für 'Algebra Test 1' wurde eingetragen: 2.00 (Gut)", new DateTime(2025, 12, 11, 9, 11, 25, 711, DateTimeKind.Utc).AddTicks(4109), "Neue Note verfügbar", 2, "45ea76af-3cb3-4a3f-a1ae-69783105c971" });

            migrationBuilder.InsertData(
                table: "teacher_subjects",
                columns: new[] { "id", "assigned_at", "subject_id", "teacher_id" },
                values: new object[,]
                {
                    { "91c77a66-2582-425c-9070-4f4c4869bdc9", new DateTime(2025, 12, 11, 9, 11, 25, 576, DateTimeKind.Utc).AddTicks(3643), "95733880-5dd3-41bd-92d3-b6690ab2edcf", "87a934f4-1428-47aa-9368-bdbade74b9ec" },
                    { "a69cfaf5-c828-47d6-ae0a-728c19f2670d", new DateTime(2025, 12, 11, 9, 11, 25, 576, DateTimeKind.Utc).AddTicks(3879), "390c0f0b-c57f-4cba-8229-82e0d3c670f8", "87a934f4-1428-47aa-9368-bdbade74b9ec" }
                });

            migrationBuilder.InsertData(
                table: "student_classes",
                columns: new[] { "id", "class_id", "enrolled_at", "student_id" },
                values: new object[] { "5991c8ed-8000-4be9-a691-ce8f2f2aa0a4", "4ed42a29-bb73-4d1a-8a34-f0463a54098c", new DateTime(2025, 12, 11, 9, 11, 25, 710, DateTimeKind.Utc).AddTicks(3778), "45ea76af-3cb3-4a3f-a1ae-69783105c971" });

            migrationBuilder.InsertData(
                table: "tests",
                columns: new[] { "id", "class_id", "created_at", "date", "description", "max_points", "name", "subject_id", "teacher_id", "type", "updated_at" },
                values: new object[] { "d82d4ae1-4b46-4a7b-b6f4-4f3e1af74e64", "4ed42a29-bb73-4d1a-8a34-f0463a54098c", new DateTime(2025, 12, 11, 9, 11, 25, 710, DateTimeKind.Utc).AddTicks(7371), new DateTime(2025, 12, 4, 9, 11, 25, 710, DateTimeKind.Utc).AddTicks(6441), "Lineare Gleichungen und Funktionen", 100, "Algebra Test 1", "95733880-5dd3-41bd-92d3-b6690ab2edcf", "87a934f4-1428-47aa-9368-bdbade74b9ec", 0, new DateTime(2025, 12, 11, 9, 11, 25, 710, DateTimeKind.Utc).AddTicks(7577) });

            migrationBuilder.InsertData(
                table: "grades",
                columns: new[] { "id", "comment", "created_at", "grade_value", "max_points", "points", "status", "student_id", "test_id", "updated_at" },
                values: new object[] { "e502cf32-72f6-43f1-9a95-3852798ddf1a", "Sehr gute Leistung!", new DateTime(2025, 12, 11, 9, 11, 25, 711, DateTimeKind.Utc).AddTicks(1420), 2.00m, 100, 82, 0, "45ea76af-3cb3-4a3f-a1ae-69783105c971", "d82d4ae1-4b46-4a7b-b6f4-4f3e1af74e64", new DateTime(2025, 12, 11, 9, 11, 25, 711, DateTimeKind.Utc).AddTicks(1622) });
        }
    }
}
