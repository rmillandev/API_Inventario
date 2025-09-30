using API_Inventario.Models;

namespace API_Inventario.Repositorys.Interfaces
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        Task<bool> ExistsByCodigo(int codigo);
        Task DeleteByCodigoProducto(int codigo);
    }
}
