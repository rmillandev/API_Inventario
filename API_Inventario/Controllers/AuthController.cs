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
        public async Task<IActionResult> Login([FromBody] LoginDTO dto, [FromServices] IValidator<LoginDTO> validator)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(dto);

                if (!validationResult.IsValid) return BadRequest(validationResult);

                var user = await usuarioService.ValidateUser(dto);

                if (user == null) return Unauthorized(new { Message = "Credenciales invalidas." });

                var token = tokenService.GenerarToken(user);

                return Ok(new AuthResponseDTO 
                {
                    Token = token,
                    Username = user.Username,
                    Rol = user.Rol,
                    Email = user.Email
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error interno del servidor: {ex.Message} - {ex.InnerException}" });
            }

        }
    }
}
