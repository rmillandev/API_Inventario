using API_Inventario.Dtos.UsuarioDtos;
using API_Inventario.Models;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Services.Interfaces
{
    public interface IUsuarioService : IGenericService<Usuario>
    {
        Task<PagedResult<ReadUsuarioDTO>> GetAllDto(int? pageNumber, int? pageSize);
        Task<ShowSuccessCreateUserDTO> CreateUsuario(CreateUsuarioDTO usuarioDto);
        Task UpdateUsuario(int id, UpdateUsuarioDTO usuarioDto);
        Task<Usuario> ValidateUser(LoginDTO dto);
    }
}
