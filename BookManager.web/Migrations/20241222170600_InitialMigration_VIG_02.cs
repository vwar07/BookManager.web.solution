using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookManager.web.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_VIG_02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "Email", "FirstName", "IsDeleted", "LastName", "PhoneNumber", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 22, 22, 35, 58, 996, DateTimeKind.Local).AddTicks(1497), null, "john.doe@example.com", "John", false, "Doe", "+1234567890", null },
                    { 2, new DateTime(2024, 12, 22, 22, 35, 58, 996, DateTimeKind.Local).AddTicks(2648), null, "jane.smith@example.com", "Jane", false, "Smith", "+1987654321", null },
                    { 3, new DateTime(2024, 12, 22, 22, 35, 58, 996, DateTimeKind.Local).AddTicks(2652), null, "emily.johnson@example.com", "Emily", false, "Johnson", "+1123456789", null },
                    { 4, new DateTime(2024, 12, 22, 22, 35, 58, 996, DateTimeKind.Local).AddTicks(2654), null, "michael.brown@example.com", "Michael", false, "Brown", "+1445566778", null },
                    { 5, new DateTime(2024, 12, 22, 22, 35, 58, 996, DateTimeKind.Local).AddTicks(2655), null, "robert.martin@example.com", "Robert", false, "Martin", "+1556677889", null },
                    { 6, new DateTime(2024, 12, 22, 22, 35, 58, 996, DateTimeKind.Local).AddTicks(2656), null, "adam.freeman@example.com", "Adam", false, "Freeman", "+1667788990", null },
                    { 7, new DateTime(2024, 12, 22, 22, 35, 58, 996, DateTimeKind.Local).AddTicks(2658), null, "andrew.troelsen@example.com", "Andrew", false, "Troelsen", "+1778899001", null },
                    { 8, new DateTime(2024, 12, 22, 22, 35, 58, 996, DateTimeKind.Local).AddTicks(2659), null, "andrew.hunt@example.com", "Andrew", false, "Hunt", "+1889900112", null },
                    { 9, new DateTime(2024, 12, 22, 22, 35, 58, 996, DateTimeKind.Local).AddTicks(2660), null, "martin.fowler@example.com", "Martin", false, "Fowler", "+1990011223", null },
                    { 10, new DateTime(2024, 12, 22, 22, 35, 58, 996, DateTimeKind.Local).AddTicks(2662), null, "eric.freeman@example.com", "Eric", false, "Freeman", "+1101122334", null }
                });

            migrationBuilder.InsertData(
                table: "Publisher",
                columns: new[] { "Id", "BuildingAndStreet", "Country", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "Name", "State", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, "80 Strand, London", "United Kingdom", new DateTime(2024, 12, 22, 22, 35, 58, 993, DateTimeKind.Local).AddTicks(4921), null, false, "Penguin", "London", null },
                    { 2, "1005 Gravenstein Highway North", "United States", new DateTime(2024, 12, 22, 22, 35, 58, 994, DateTimeKind.Local).AddTicks(9722), null, false, "O'Reilly Media", "California", null },
                    { 3, "Livery Place, 35 Livery Street", "United Kingdom", new DateTime(2024, 12, 22, 22, 35, 58, 994, DateTimeKind.Local).AddTicks(9743), null, false, "Packt Publishing", "Birmingham", null },
                    { 4, "One Microsoft Way", "United States", new DateTime(2024, 12, 22, 22, 35, 58, 994, DateTimeKind.Local).AddTicks(9745), null, false, "Microsoft Press", "Washington", null },
                    { 5, "501 Boylston Street", "United States", new DateTime(2024, 12, 22, 22, 35, 58, 994, DateTimeKind.Local).AddTicks(9746), null, false, "Prentice Hall", "Massachusetts", null },
                    { 6, "233 Spring Street", "United States", new DateTime(2024, 12, 22, 22, 35, 58, 994, DateTimeKind.Local).AddTicks(9748), null, false, "Apress", "New York", null },
                    { 7, "625 Avenue of the Americas", "United States", new DateTime(2024, 12, 22, 22, 35, 58, 994, DateTimeKind.Local).AddTicks(9750), null, false, "Apress", "New York", null },
                    { 8, "75 Arlington Street", "United States", new DateTime(2024, 12, 22, 22, 35, 58, 994, DateTimeKind.Local).AddTicks(9752), null, false, "Addison-Wesley", "Massachusetts", null },
                    { 9, "501 Boylston Street", "United States", new DateTime(2024, 12, 22, 22, 35, 58, 994, DateTimeKind.Local).AddTicks(9753), null, false, "Addison-Wesley", "Massachusetts", null },
                    { 10, "1005 Gravenstein Highway North", "United States", new DateTime(2024, 12, 22, 22, 35, 58, 994, DateTimeKind.Local).AddTicks(9754), null, false, "O'Reilly Media", "California", null }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "AuthorId", "AvailableQuantity", "CreatedDateTime", "DeletedDateTime", "Description", "ImageLocation", "IsAvailable", "IsDeleted", "Price", "PublisherId", "Title", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, 1, 50, new DateTime(2024, 12, 22, 22, 35, 58, 997, DateTimeKind.Local).AddTicks(1313), null, "A comprehensive guide to learning the C# programming language, covering basics to advanced concepts.", "/images/csharp_programming.jpg", true, false, 29.99m, 1, "C# Programming", null },
                    { 2, 2, 0, new DateTime(2024, 12, 22, 22, 35, 58, 997, DateTimeKind.Local).AddTicks(3685), null, "An advanced book for developers to master .NET Core framework and build scalable applications.", "/images/mastering_dotnet_core.jpg", false, false, 39.99m, 2, "Mastering .NET Core", null },
                    { 3, 3, 20, new DateTime(2024, 12, 22, 22, 35, 58, 997, DateTimeKind.Local).AddTicks(3691), null, "Learn how to use Entity Framework to handle database operations efficiently in your .NET applications.", "/images/entity_framework_essentials.jpg", true, false, 24.99m, 3, "Entity Framework Essentials", null },
                    { 4, 4, 25, new DateTime(2024, 12, 22, 22, 35, 58, 997, DateTimeKind.Local).AddTicks(3792), null, "A beginner-friendly book to understand and build web applications using ASP.NET Core.", "/images/learning_aspnet_core.jpg", true, false, 34.99m, 4, "Learning ASP.NET Core", null },
                    { 5, 5, 0, new DateTime(2024, 12, 22, 22, 35, 58, 997, DateTimeKind.Local).AddTicks(3795), null, "A practical guide to implementing clean architecture principles for robust and maintainable codebases.", "/images/clean_architecture.jpg", false, false, 49.99m, 5, "Clean Architecture", null },
                    { 6, 6, 40, new DateTime(2024, 12, 22, 22, 35, 58, 997, DateTimeKind.Local).AddTicks(3797), null, "A professional guide to building modern web applications using ASP.NET Core MVC framework.", "/images/pro_aspnet_core_mvc.jpg", true, false, 44.99m, 6, "Pro ASP.NET Core MVC", null },
                    { 7, 7, 35, new DateTime(2024, 12, 22, 22, 35, 58, 997, DateTimeKind.Local).AddTicks(3800), null, "An in-depth exploration of C# programming, designed for both novice and experienced developers.", "/images/programming_in_csharp.jpg", true, false, 54.99m, 7, "Programming in C#", null },
                    { 8, 8, 45, new DateTime(2024, 12, 22, 22, 35, 58, 997, DateTimeKind.Local).AddTicks(3802), null, "A timeless guide to software development best practices and tips for pragmatic programmers.", "/images/the_pragmatic_programmer.jpg", true, false, 42.99m, 8, "The Pragmatic Programmer", null },
                    { 9, 9, 28, new DateTime(2024, 12, 22, 22, 35, 58, 997, DateTimeKind.Local).AddTicks(3804), null, "An insightful book on improving the design of existing code without changing its functionality.", "/images/refactoring.jpg", true, false, 39.99m, 9, "Refactoring", null },
                    { 10, 10, 22, new DateTime(2024, 12, 22, 22, 35, 58, 997, DateTimeKind.Local).AddTicks(3806), null, "A visually engaging book that simplifies complex design patterns for easier understanding and application.", "/images/head_first_design_patterns.jpg", true, false, 49.99m, 10, "Head First Design Patterns", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
