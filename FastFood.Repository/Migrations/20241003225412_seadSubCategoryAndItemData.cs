using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastFood.Repository.Migrations
{
    /// <inheritdoc />
    public partial class seadSubCategoryAndItemData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Beef Burgers" },
                    { 2, 1, "Chicken Burgers" },
                    { 3, 2, "Soft Drinks" },
                    { 4, 2, "Juices" },
                    { 5, 3, "Ice Cream" },
                    { 6, 3, "Cakes" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "Description", "Price", "SubCategoryId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Juicy beef patty with cheese, lettuce, and tomato.", 5.9900000000000002, 1, "Cheeseburger" },
                    { 2, 1, "Grilled chicken breast with lettuce and mayo.", 4.9900000000000002, 2, "Chicken Sandwich" },
                    { 3, 2, "Chilled soft drink.", 1.99, 3, "Cola" },
                    { 4, 2, "Freshly squeezed orange juice.", 2.9900000000000002, 4, "Orange Juice" },
                    { 5, 3, "Creamy vanilla-flavored ice cream.", 3.5, 5, "Vanilla Ice Cream" },
                    { 6, 3, "Rich chocolate cake with a smooth frosting.", 4.5, 6, "Chocolate Cake" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
