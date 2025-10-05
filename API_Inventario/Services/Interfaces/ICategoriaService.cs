using API_Inventario.Dtos.CategoriaDtos;
using API_Inventario.Models;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Services.Interfaces
{
    public interface ICategoriaService : IGenericService<Categoria>
    {
        Task<CreateSuccessResponse<CreateCategoriaDTO>> CreateCategoria(CreateCategoriaDTO categoriaDto);
        Task UpdateCategoria(int id, UpdateCategoriaDTO categoriaDto);
    }
}
