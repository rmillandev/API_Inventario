namespace API_Inventario.Dtos.ProductoDtos
{
    public class UpdateProductoDTO
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? StockActual { get; set; }
        public int? CategoriaId { get; set; }
        public int? ProveedorId { get; set; }
    }
}
