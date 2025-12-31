using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace INDWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { new Guid("0d9c8b7a-6f5e-4d3c-2b1a-0f9e8d7c6b5a"), "Hard" },
                    { new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), "Medium" },
                    { new Guid("9c3a1d2e-4f5b-4c6d-8e7f-1a2b3c4d5e6f"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "ID", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("0d9c8b7a-6f5e-4d3c-2b1a-0f9e8d7c6b5a"), "HP", "Himanchal Pradesh", "https://www.istockphoto.com/photos/himanchal-pradesh" },
                    { new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"), "SK", "Sikkim", "https://www.istockphoto.com/photos/sikkim" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "ID",
                keyValue: new Guid("0d9c8b7a-6f5e-4d3c-2b1a-0f9e8d7c6b5a"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "ID",
                keyValue: new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "ID",
                keyValue: new Guid("9c3a1d2e-4f5b-4c6d-8e7f-1a2b3c4d5e6f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "ID",
                keyValue: new Guid("0d9c8b7a-6f5e-4d3c-2b1a-0f9e8d7c6b5a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "ID",
                keyValue: new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"));
        }
    }
}
