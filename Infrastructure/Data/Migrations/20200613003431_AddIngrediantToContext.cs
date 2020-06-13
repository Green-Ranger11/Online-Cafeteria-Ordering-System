using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddIngrediantToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingrediant_Meals_MealId",
                table: "Ingrediant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingrediant",
                table: "Ingrediant");

            migrationBuilder.RenameTable(
                name: "Ingrediant",
                newName: "Ingrediants");

            migrationBuilder.RenameIndex(
                name: "IX_Ingrediant_MealId",
                table: "Ingrediants",
                newName: "IX_Ingrediants_MealId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingrediants",
                table: "Ingrediants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingrediants_Meals_MealId",
                table: "Ingrediants",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingrediants_Meals_MealId",
                table: "Ingrediants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingrediants",
                table: "Ingrediants");

            migrationBuilder.RenameTable(
                name: "Ingrediants",
                newName: "Ingrediant");

            migrationBuilder.RenameIndex(
                name: "IX_Ingrediants_MealId",
                table: "Ingrediant",
                newName: "IX_Ingrediant_MealId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingrediant",
                table: "Ingrediant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingrediant_Meals_MealId",
                table: "Ingrediant",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
