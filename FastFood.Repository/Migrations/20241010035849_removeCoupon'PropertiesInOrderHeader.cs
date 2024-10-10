using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Repository.Migrations
{
    /// <inheritdoc />
    public partial class removeCouponPropertiesInOrderHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "orderHeaders");

            migrationBuilder.DropColumn(
                name: "CouponDis",
                table: "orderHeaders");

            migrationBuilder.DropColumn(
                name: "TransId",
                table: "orderHeaders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CouponCode",
                table: "orderHeaders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CouponDis",
                table: "orderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TransId",
                table: "orderHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
