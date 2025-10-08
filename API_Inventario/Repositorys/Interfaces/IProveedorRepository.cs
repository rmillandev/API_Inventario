using API_Inventario.Dtos.ProveedorDTO;
using API_Inventario.Models;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Repositorys.Interfaces
{
    public interface IProveedorRepository : IGenericRepository<Proveedor> 
    {
        Task<bool> ExistsByNit(string nit);

    }
}
