using API_Inventario.Dtos.CategoriaDtos;
using API_Inventario.Models;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Repositorys.Interfaces
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        Task<bool> ExistsByName(string name); 
    }
}
