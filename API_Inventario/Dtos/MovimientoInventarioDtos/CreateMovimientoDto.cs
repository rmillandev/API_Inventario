namespace API_Inventario.Dtos.MovimientoInventarioDtos
{
    public class CreateMovimientoDto
    {
        public required int Cantidad { get; set; }
        public required string TipoMovimiento { get; set; }
        public required string UsuarioResponsable { get; set; }
        public required int ProductoId { get; set; }
    }
}
