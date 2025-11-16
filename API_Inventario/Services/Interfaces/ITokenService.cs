using API_Inventario.Models;

namespace API_Inventario.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerarToken(Usuario usuario);
    }
}
