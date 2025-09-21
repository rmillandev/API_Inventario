using API_Inventario.Db;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Inventario.Repositorys
{
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedorRepository
    {

        private readonly Context context;

        public ProveedorRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> ExistsByNit(string nit)
        {
            return await context.Proveedor.AnyAsync(p => p.Nit == nit);
        }
    }
}
