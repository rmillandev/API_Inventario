using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Inventario.Migrations
{
    /// <inheritdoc />
    public partial class ColumnaNitAggEnProveedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nit",
                table: "Proveedor",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nit",
                table: "Proveedor");
        }
    }
}
