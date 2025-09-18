using System.ComponentModel.DataAnnotations;

namespace API_Inventario.Models
{
    public class MovimientoInventario
    {
        public int Id { get; set; }

        [Required]
        public required int Cantidad { get; set; }

        [Required]
        [StringLength(50)]
        public required string TipoMovimiento { get; set; }

        [Required]
        [StringLength(250)]
        public required string UsuarioResponsable { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;


        [Required]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

    }
}
