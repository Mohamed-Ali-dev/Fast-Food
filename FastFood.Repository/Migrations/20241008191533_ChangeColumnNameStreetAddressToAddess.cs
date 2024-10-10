using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNameStreetAddressToAddess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "orderHeaders",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "orderHeaders",
                newName: "StreetAddress");
        }
    }
}
