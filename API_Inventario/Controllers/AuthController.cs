using API_Inventario.Dtos.UsuarioDtos;
using API_Inventario.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace API_Inventario.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;
        private readonly ITokenService tokenService;

        public AuthController(IUsuarioService usuarioService, ITokenService tokenService)
        {
            this.usuarioService = usuarioService;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var user = await usuarioService.ValidateUser(dto);
            var token = tokenService.GenerarToken(user);

            return Ok(new AuthResponseDTO
            {
                Token = token,
                Username = user.Username,
                Rol = user.Rol,
                Email = user.Email
            });
        }
    }
}
