using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3589d4f3-5b40-4670-bc84-02439b8510ff"), "Easy" },
                    { new Guid("42492b05-5f1f-490a-ab1b-e60e0414fd0f"), "Medium" },
                    { new Guid("c4c35939-1442-4700-a600-8c516fcd6d54"), "Hard" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3589d4f3-5b40-4670-bc84-02439b8510ff"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("42492b05-5f1f-490a-ab1b-e60e0414fd0f"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c4c35939-1442-4700-a600-8c516fcd6d54"));
        }
    }
}
