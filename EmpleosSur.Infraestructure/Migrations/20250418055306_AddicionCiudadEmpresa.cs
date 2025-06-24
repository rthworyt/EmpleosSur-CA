using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpleosSur.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddicionCiudadEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Empresas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Empresas");
        }
    }
}
