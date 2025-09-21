using System.ComponentModel.DataAnnotations;

namespace API_Inventario.Models
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public required string Nit {  get; set; }

        [Required]
        [StringLength(250)]
        public required string Nombre { get; set; }

        [Required]
        [StringLength(20)]
        public required string Telefono { get; set; }

        [Required]
        [StringLength(250)]
        public required string Email { get; set; }

        [StringLength(100)]
        public string? Direccion { get; set; }
    }
}
