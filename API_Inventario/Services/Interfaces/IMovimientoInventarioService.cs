using API_Inventario.Dtos.MovimientoInventarioDtos;
using API_Inventario.Models;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Services.Interfaces
{
    public interface IMovimientoInventarioService : IGenericService<MovimientoInventario>
    {
        Task<CreateSuccessResponse<CreateMovimientoDto>> CreateMovimientoInventario(CreateMovimientoDto createMovimientoDto);
    }
}
