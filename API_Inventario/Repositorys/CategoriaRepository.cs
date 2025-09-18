using API_Inventario.Db;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Inventario.Repositorys
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        private readonly Context context;

        public CategoriaRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> ExistsByName(string name)
        {
            return await context.Categoria.AnyAsync(c => c.Nombre == name);
        }
    }
}
