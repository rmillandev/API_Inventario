using API_Inventario.Models;

namespace API_Inventario.Repositorys.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        bool UsernameAlreadyExist(string username);
        Task<Usuario?> GetByUsername(string username);
    }
}
