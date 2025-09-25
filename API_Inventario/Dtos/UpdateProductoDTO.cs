namespace API_Inventario.Dtos
{
    public class UpdateProductoDTO
    {
        public int Id { get; set; }
        public int? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? StockActual { get; set; }
        public int? StockMinimo { get; set; }
        public bool? Activo { get; set; }
        public int? CategoriaId { get; set; }
        public int? ProveedorId { get; set; }
    }
}
