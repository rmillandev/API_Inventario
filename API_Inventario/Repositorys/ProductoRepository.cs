using API_Inventario.Db;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Utils.Exceptions;
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

        public async Task<Producto?> GetByCodigo(int codigo)
        {
            return await entities.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public async Task DeleteByCodigoProducto(int codigo)
        {
            int deleted = await entities.Where(p => p.Codigo == codigo).ExecuteDeleteAsync();
            if (deleted == 0) throw new BusinessException("Este codigo de producto no existe.");
        }

        public async Task UpdateProducto(Producto producto)
        {
            entities.Update(producto);
            await context.SaveChangesAsync();
        }
    }
}
