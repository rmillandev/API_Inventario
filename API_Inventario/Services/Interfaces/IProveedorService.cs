using API_Inventario.Dtos;
using API_Inventario.Models;

namespace API_Inventario.Services.Interfaces
{
    public interface IProveedorService : IGenericService<Proveedor>
    {
        Task UpdateProveedor(int id, ProveedorDTO proveedor);
    }
}
