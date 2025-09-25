using API_Inventario.Models;

namespace API_Inventario.Dtos
{
    public class CreateProductoDTO
    {
        public int Codigo { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public bool Activo { get; set; }
        public int CategoriaId { get; set; }
        public int ProveedorId { get; set; }
    }
}
