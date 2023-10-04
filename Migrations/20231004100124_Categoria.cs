using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    /// <inheritdoc />
    public partial class Categoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Pizze",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pizze_CategoriaId",
                table: "Pizze",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizze_Categorie_CategoriaId",
                table: "Pizze",
                column: "CategoriaId",
                principalTable: "Categorie",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizze_Categorie_CategoriaId",
                table: "Pizze");

            migrationBuilder.DropIndex(
                name: "IX_Pizze_CategoriaId",
                table: "Pizze");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Pizze");
        }
    }
}
