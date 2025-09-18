using System.ComponentModel.DataAnnotations;

namespace API_Inventario.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public required string Nombre { get; set; }

        [Required]
        [StringLength(500)]
        public required string Descripcion {  get; set; }
    }
}
