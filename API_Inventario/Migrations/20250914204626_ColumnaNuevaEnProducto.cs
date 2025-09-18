using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Inventario.Migrations
{
    /// <inheritdoc />
    public partial class ColumnaNuevaEnProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Producto");
        }
    }
}
