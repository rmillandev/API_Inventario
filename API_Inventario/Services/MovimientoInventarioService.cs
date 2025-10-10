using API_Inventario.Dtos.MovimientoInventarioDtos;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Constanst;
using API_Inventario.Utils.Exceptions;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Services
{
    public class MovimientoInventarioService : GenericService<MovimientoInventario>, IMovimientoInventarioService
    {
        private readonly IMovimientoInventarioRepository repository;
        private readonly IProductoService productoService;
        public MovimientoInventarioService(IMovimientoInventarioRepository repository, IProductoService productoService) : base(repository)
        {
            this.repository = repository;
            this.productoService = productoService;
        }

        public async Task<CreateSuccessResponse<CreateMovimientoDto>> CreateMovimientoInventario(CreateMovimientoDto createMovimientoDto)
        {
            
            if (createMovimientoDto.TipoMovimiento != ConstantsMovimientoInventario.TIPO_MOVIMIENTO_ENTRADA && createMovimientoDto.TipoMovimiento != ConstantsMovimientoInventario.TIPO_MOVIMIENTO_SALIDA)
            {
                return new CreateSuccessResponse<CreateMovimientoDto> 
                {
                    Success = false,
                    Message = "Tipo de movimiento invalido."
                };
            }

            await productoService.UpdateStockProduct(createMovimientoDto.ProductoId, createMovimientoDto.Cantidad, createMovimientoDto.TipoMovimiento);

            MovimientoInventario movimiento = new MovimientoInventario 
            {
                Cantidad = createMovimientoDto.Cantidad,
                TipoMovimiento = createMovimientoDto.TipoMovimiento,
                UsuarioResponsable = createMovimientoDto.UsuarioResponsable,
                ProductoId = createMovimientoDto.ProductoId
            };

            await repository.Create(movimiento);

            return new CreateSuccessResponse<CreateMovimientoDto> 
            {
                Success = true,
                Message = "Movimiento creado exitosamente.",
                Data = createMovimientoDto
            };

        }
    }
}
