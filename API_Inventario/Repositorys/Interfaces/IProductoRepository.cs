using API_Inventario.Models;

namespace API_Inventario.Repositorys.Interfaces
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        Task<bool> ExistsByCodigo(int codigo);
        Task<Producto?> GetByCodigo(int codigo); 
        Task<int> DeleteByCodigoProducto(int codigo);
        Task UpdateProducto(Producto producto);
    }
}
