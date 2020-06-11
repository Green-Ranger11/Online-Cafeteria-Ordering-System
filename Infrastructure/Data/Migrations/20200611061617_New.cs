using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Menus_MenuId1",
                table: "Meals");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "shippingDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Menus_MenuId1",
                table: "Meals",
                column: "MenuId1",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Menus_MenuId1",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "shippingDate",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Menus_MenuId1",
                table: "Meals",
                column: "MenuId1",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
