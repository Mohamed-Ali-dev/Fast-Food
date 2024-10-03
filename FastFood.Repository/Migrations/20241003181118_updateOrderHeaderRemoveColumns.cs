using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderHeaderRemoveColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfPick",
                table: "orderHeaders");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "orderHeaders");

            migrationBuilder.DropColumn(
                name: "TransId",
                table: "orderHeaders");

            migrationBuilder.RenameColumn(
                name: "TimeOfPick",
                table: "orderHeaders",
                newName: "ShippingDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingDate",
                table: "orderHeaders",
                newName: "TimeOfPick");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfPick",
                table: "orderHeaders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "SubTotal",
                table: "orderHeaders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TransId",
                table: "orderHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
