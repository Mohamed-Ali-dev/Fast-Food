using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Repository.Migrations
{
    /// <inheritdoc />
    public partial class EditColumnNameCouponPictureToCouponImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CouponPicture",
                table: "Coupons",
                newName: "CouponImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CouponImage",
                table: "Coupons",
                newName: "CouponPicture");
        }
    }
}
