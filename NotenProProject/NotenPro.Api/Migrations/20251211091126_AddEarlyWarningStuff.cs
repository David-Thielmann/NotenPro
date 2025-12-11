using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotenPro.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEarlyWarningStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "classes",
                keyColumn: "id",
                keyValue: "997fc220-5101-4398-afbb-9c49cb4ed86e");

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: "372991f5-45f8-40b4-8b5d-dd839193d93a");

            migrationBuilder.DeleteData(
                table: "notifications",
                keyColumn: "id",
                keyValue: "86dddf77-de25-4788-8436-fa3c587a7812");

            migrationBuilder.DeleteData(
                table: "student_classes",
                keyColumn: "id",
                keyValue: "8f9b0ead-34f8-49bb-9b4b-6f35086b9aae");

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: "3bb45453-a791-41a0-a506-2451d31cdbf1");

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: "61eac821-34a8-418c-88a2-358c00e4919e");

            migrationBuilder.DeleteData(
                table: "teacher_subjects",
                keyColumn: "id",
                keyValue: "5f87493a-d2aa-4d24-ae4b-ca7badb1feea");

            migrationBuilder.DeleteData(
                table: "teacher_subjects",
                keyColumn: "id",
                keyValue: "d0a84f11-82f7-41c8-a4bc-527fad8d7eae");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "b908b045-d177-47ed-b773-a2847c1ca9ff");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "e192d14a-5e75-496e-9c8d-03ba1e30941e");

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: "b8eba63c-e101-41d0-adda-868fbb38fd57");

            migrationBuilder.DeleteData(
                table: "tests",
                keyColumn: "id",
                keyValue: "c56e367c-6d24-41ca-be2b-2e0cd5dd1707");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "9d9816db-e63d-4074-878f-1e9419c5d479");

            migrationBuilder.DeleteData(
                table: "classes",
                keyColumn: "id",
                keyValue: "6c863c11-c696-42cf-8c6f-fff878504f04");

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: "f050dc31-4ceb-4e9f-8d12-d838e4e09a26");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: "f18280f5-b807-43a8-b76c-bafcd999c7b9");

            migrationBuilder.DeleteData(
                table: "schools",
                keyColumn: "id",
                keyValue: "0638849d-aa77-4355-8d62-7f0cd90ab6ef");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "0638849d-aa77-4355-8d62-7f0cd90ab6ef", new DateTime(2025, 12, 11, 9, 8, 23, 179, DateTimeKind.Utc).AddTicks(1180), "Krems an der Donau", "HTL Krems", "Active", new DateTime(2025, 12, 11, 9, 8, 23, 179, DateTimeKind.Utc).AddTicks(1501) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "is_active", "name", "password_hash", "role", "school_id", "updated_at" },
                values: new object[] { "e192d14a-5e75-496e-9c8d-03ba1e30941e", new DateTime(2025, 12, 11, 9, 8, 23, 178, DateTimeKind.Utc).AddTicks(1377), "sysadmin@notenpro.at", true, "System Administrator", "$2a$11$aCE4W7ukvVoCZl40CJobP./2Z3mJ0rIvCKKwbLsyBUyaX9cci42p.", 3, null, new DateTime(2025, 12, 11, 9, 8, 23, 178, DateTimeKind.Utc).AddTicks(1657) });

            migrationBuilder.InsertData(
                table: "classes",
                columns: new[] { "id", "class_teacher_id", "created_at", "name", "school_id", "updated_at" },
                values: new object[] { "997fc220-5101-4398-afbb-9c49cb4ed86e", null, new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(4567), "5BHIT", "0638849d-aa77-4355-8d62-7f0cd90ab6ef", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(4568) });

            migrationBuilder.InsertData(
                table: "subjects",
                columns: new[] { "id", "created_at", "description", "is_active", "name", "school_id", "updated_at" },
                values: new object[,]
                {
                    { "3bb45453-a791-41a0-a506-2451d31cdbf1", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(7720), "English Language", true, "Englisch", "0638849d-aa77-4355-8d62-7f0cd90ab6ef", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(7721) },
                    { "61eac821-34a8-418c-88a2-358c00e4919e", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(7713), "Deutsche Sprache und Literatur", true, "Deutsch", "0638849d-aa77-4355-8d62-7f0cd90ab6ef", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(7717) },
                    { "b8eba63c-e101-41d0-adda-868fbb38fd57", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(7727), "Software Engineering", true, "Programmieren", "0638849d-aa77-4355-8d62-7f0cd90ab6ef", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(7728) },
                    { "f050dc31-4ceb-4e9f-8d12-d838e4e09a26", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(7284), "Angewandte Mathematik", true, "Mathematik", "0638849d-aa77-4355-8d62-7f0cd90ab6ef", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(7496) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "is_active", "name", "password_hash", "role", "school_id", "updated_at" },
                values: new object[,]
                {
                    { "9d9816db-e63d-4074-878f-1e9419c5d479", new DateTime(2025, 12, 11, 9, 8, 23, 576, DateTimeKind.Utc).AddTicks(7651), "max.mustermann@students.htl-krems.ac.at", true, "Max Mustermann", "$2a$11$FDwlBiI/GUalmx9gGnf3Ue0zp5T6yVO.R/ue0gKR4Wyn2H/IZhrPi", 0, "0638849d-aa77-4355-8d62-7f0cd90ab6ef", new DateTime(2025, 12, 11, 9, 8, 23, 576, DateTimeKind.Utc).AddTicks(7656) },
                    { "b908b045-d177-47ed-b773-a2847c1ca9ff", new DateTime(2025, 12, 11, 9, 8, 23, 311, DateTimeKind.Utc).AddTicks(8574), "admin@htl-krems.ac.at", true, "HTL Admin", "$2a$11$Cb9mDXCNBR229Ew9u7ae6.II1gQtgBQC.SkBDahTSOyWKMWC8cJ1C", 2, "0638849d-aa77-4355-8d62-7f0cd90ab6ef", new DateTime(2025, 12, 11, 9, 8, 23, 311, DateTimeKind.Utc).AddTicks(8578) },
                    { "f18280f5-b807-43a8-b76c-bafcd999c7b9", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(554), "maria.schmidt@htl-krems.ac.at", true, "Prof. Maria Schmidt", "$2a$11$wndi5gosqYHATQCfbotuauSqpLIgcbcyQPSPh318eGGbu5tSpEX0u", 1, "0638849d-aa77-4355-8d62-7f0cd90ab6ef", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(557) }
                });

            migrationBuilder.InsertData(
                table: "classes",
                columns: new[] { "id", "class_teacher_id", "created_at", "name", "school_id", "updated_at" },
                values: new object[] { "6c863c11-c696-42cf-8c6f-fff878504f04", "f18280f5-b807-43a8-b76c-bafcd999c7b9", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(4038), "5AHIT", "0638849d-aa77-4355-8d62-7f0cd90ab6ef", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(4265) });

            migrationBuilder.InsertData(
                table: "notifications",
                columns: new[] { "id", "is_read", "message", "timestamp", "title", "type", "user_id" },
                values: new object[] { "86dddf77-de25-4788-8436-fa3c587a7812", false, "Deine Note für 'Algebra Test 1' wurde eingetragen: 2.00 (Gut)", new DateTime(2025, 12, 11, 9, 8, 23, 578, DateTimeKind.Utc).AddTicks(1608), "Neue Note verfügbar", 2, "9d9816db-e63d-4074-878f-1e9419c5d479" });

            migrationBuilder.InsertData(
                table: "teacher_subjects",
                columns: new[] { "id", "assigned_at", "subject_id", "teacher_id" },
                values: new object[,]
                {
                    { "5f87493a-d2aa-4d24-ae4b-ca7badb1feea", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(9206), "f050dc31-4ceb-4e9f-8d12-d838e4e09a26", "f18280f5-b807-43a8-b76c-bafcd999c7b9" },
                    { "d0a84f11-82f7-41c8-a4bc-527fad8d7eae", new DateTime(2025, 12, 11, 9, 8, 23, 447, DateTimeKind.Utc).AddTicks(9420), "b8eba63c-e101-41d0-adda-868fbb38fd57", "f18280f5-b807-43a8-b76c-bafcd999c7b9" }
                });

            migrationBuilder.InsertData(
                table: "student_classes",
                columns: new[] { "id", "class_id", "enrolled_at", "student_id" },
                values: new object[] { "8f9b0ead-34f8-49bb-9b4b-6f35086b9aae", "6c863c11-c696-42cf-8c6f-fff878504f04", new DateTime(2025, 12, 11, 9, 8, 23, 577, DateTimeKind.Utc).AddTicks(374), "9d9816db-e63d-4074-878f-1e9419c5d479" });

            migrationBuilder.InsertData(
                table: "tests",
                columns: new[] { "id", "class_id", "created_at", "date", "description", "max_points", "name", "subject_id", "teacher_id", "type", "updated_at" },
                values: new object[] { "c56e367c-6d24-41ca-be2b-2e0cd5dd1707", "6c863c11-c696-42cf-8c6f-fff878504f04", new DateTime(2025, 12, 11, 9, 8, 23, 577, DateTimeKind.Utc).AddTicks(5673), new DateTime(2025, 12, 4, 9, 8, 23, 577, DateTimeKind.Utc).AddTicks(4657), "Lineare Gleichungen und Funktionen", 100, "Algebra Test 1", "f050dc31-4ceb-4e9f-8d12-d838e4e09a26", "f18280f5-b807-43a8-b76c-bafcd999c7b9", 0, new DateTime(2025, 12, 11, 9, 8, 23, 577, DateTimeKind.Utc).AddTicks(5865) });

            migrationBuilder.InsertData(
                table: "grades",
                columns: new[] { "id", "comment", "created_at", "grade_value", "max_points", "points", "status", "student_id", "test_id", "updated_at" },
                values: new object[] { "372991f5-45f8-40b4-8b5d-dd839193d93a", "Sehr gute Leistung!", new DateTime(2025, 12, 11, 9, 8, 23, 577, DateTimeKind.Utc).AddTicks(9136), 2.00m, 100, 82, 0, "9d9816db-e63d-4074-878f-1e9419c5d479", "c56e367c-6d24-41ca-be2b-2e0cd5dd1707", new DateTime(2025, 12, 11, 9, 8, 23, 577, DateTimeKind.Utc).AddTicks(9321) });
        }
    }
}
