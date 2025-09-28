using API_Inventario.Db;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Inventario.Repositorys
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        private readonly Context context;
        public ProductoRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> ExistsByCodigo(int codigo)
        {
            return await entities.AnyAsync(p => p.Codigo == codigo);
        }
    }
}
