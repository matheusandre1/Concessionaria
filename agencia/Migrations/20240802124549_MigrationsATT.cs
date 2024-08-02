using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace agencia.Migrations
{
    /// <inheritdoc />
    public partial class MigrationsATT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administradores",
                keyColumn: "Id",
                keyValue: 1,
                column: "Perfil",
                value: "Adm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administradores",
                keyColumn: "Id",
                keyValue: 1,
                column: "Perfil",
                value: "Adn");
        }
    }
}
