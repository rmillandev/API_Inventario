using API_Inventario.Models;

namespace API_Inventario.Repositorys.Interfaces
{
    public interface IProveedorRepository : IGenericRepository<Proveedor> 
    {
        Task<bool> ExistsByNit(string nit);

    }
}
