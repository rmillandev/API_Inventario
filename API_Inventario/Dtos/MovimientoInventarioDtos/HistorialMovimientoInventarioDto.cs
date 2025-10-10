namespace API_Inventario.Dtos.MovimientoInventarioDtos
{
    public class HistorialMovimientoInventarioDto
    {
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public string ResponsableMovimiento { get; set; }
    }
}
