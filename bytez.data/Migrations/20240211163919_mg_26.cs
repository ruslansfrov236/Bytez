using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bytez.data.Migrations
{
    public partial class mg_26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Order_OrdersId",
                table: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Basket_OrdersId",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "Basket");

            migrationBuilder.AddColumn<Guid>(
                name: "BasketId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Order_BasketId",
                table: "Order",
                column: "BasketId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Basket_BasketId",
                table: "Order",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Basket_BasketId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_BasketId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "Order");

            migrationBuilder.AddColumn<Guid>(
                name: "OrdersId",
                table: "Basket",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Basket_OrdersId",
                table: "Basket",
                column: "OrdersId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Order_OrdersId",
                table: "Basket",
                column: "OrdersId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
