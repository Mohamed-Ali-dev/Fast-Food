using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastFood.Repository.Migrations
{
    /// <inheritdoc />
    public partial class seadCategoryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "orderHeaders");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "orderHeaders",
                newName: "StreetAddress");

            migrationBuilder.AddColumn<int>(
                name: "TransId",
                table: "orderHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Burgers" },
                    { 2, "Drinks" },
                    { 3, "Desserts" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "TransId",
                table: "orderHeaders");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "orderHeaders",
                newName: "Address");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "orderHeaders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
