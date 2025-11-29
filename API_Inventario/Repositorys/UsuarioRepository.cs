using API_Inventario.Db;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Inventario.Repositorys
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly Context context;
        public UsuarioRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public bool UsernameAlreadyExist(string username)
        {
            return context.Usuario.Any(u => u.Username == username);
        }

        public bool EmailAlreadyExist(string email)
        {
            return context.Usuario.Any(u => u.Email == email);
        }

        public async Task<Usuario?> GetByUsername(string username)
        {
            return await context.Usuario.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
