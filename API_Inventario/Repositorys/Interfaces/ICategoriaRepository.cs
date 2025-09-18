using API_Inventario.Models;

namespace API_Inventario.Repositorys.Interfaces
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        Task<bool> ExistsByName(string name); 
    }
}
