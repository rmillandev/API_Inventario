using System.ComponentModel.DataAnnotations;

namespace API_Inventario.Dtos.ProveedorDTO
{
    public class CreateProveedorDTO
    {
        public required string Nit { get; set; }
        public required string Nombre { get; set; }
        public required string Telefono { get; set; }
        public required string Email { get; set; }
        public string? Direccion { get; set; }
    }
}
