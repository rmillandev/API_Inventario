using API_Inventario.Dtos.ProveedorDTO;
using API_Inventario.Dtos.ProveedorDtos;
using API_Inventario.Models;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Services.Interfaces
{
    public interface IProveedorService : IGenericService<Proveedor>
    {
        Task<PagedResult<ReadProveedorDTO>> GetAllDto(int? pageNumber, int? pageSize);
        Task<CreateSuccessResponse<CreateProveedorDTO>> CreateProveedor(CreateProveedorDTO proveedorDto);
        Task UpdateProveedor(int id, UpdateProveedorDTO proveedor);
    }
}
