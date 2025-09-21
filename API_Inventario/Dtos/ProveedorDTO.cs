using System.ComponentModel.DataAnnotations;

namespace API_Inventario.Dtos
{
    public class ProveedorDTO
    {
        public int Id { get; set; }
        public string? Nit { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
    }
}
