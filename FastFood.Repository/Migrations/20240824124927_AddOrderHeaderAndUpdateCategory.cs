using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderHeaderAndUpdateCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orderHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOfPick = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfPick = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubTotal = table.Column<double>(type: "float", nullable: false),
                    OrderTotal = table.Column<double>(type: "float", nullable: false),
                    CouponCode = table.Column<double>(type: "float", nullable: false),
                    CouponDis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransId = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderHeaders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderHeaders_ApplicationUserId",
                table: "orderHeaders",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderHeaders");
        }
    }
}
