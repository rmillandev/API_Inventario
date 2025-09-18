using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API_Inventario.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        public int Codigo { get; set; }

        [Required]
        [StringLength(250)]
        public required string Nombre { get; set; }

        [Required]
        [StringLength(500)]
        public required string Descripcion { get; set; }

        [Required]
        [Precision(18, 2)]
        public required decimal Precio { get; set; }

        [Required]
        public required int StockActual { get; set; }

        [Required]
        public required int StockMinimo { get; set; }

        [Required]
        public required bool Activo {  get; set; }


        [Required]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;


        [Required]
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; } = null!;


        public ICollection<MovimientoInventario> Movimientos { get; set; } = new List<MovimientoInventario>();

    }
}
