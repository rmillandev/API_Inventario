namespace API_Inventario.Dtos.ProductoDtos
{
    public class ReadProductoDTO
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int StockActual { get; set; }
        public string Categoria { get; set; }
        public string Proveedor { get; set; }
    }
}
