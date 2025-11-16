using API_Inventario.Dtos.UsuarioDtos;
using API_Inventario.Services.Interfaces;
using API_Inventario.Validations.UsuarioValidation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace API_Inventario.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        
        private readonly IUsuarioService service;

        public UsuarioController(IUsuarioService service)
        {
            this.service = service;
        }


        [HttpPost]
        public async Task<ActionResult<ShowSuccessCreateUserDTO>> CreateUsuario([FromBody] CreateUsuarioDTO usuarioDto, [FromServices] IValidator<CreateUsuarioDTO> validator)
        {
            try
            {
                var resultValidator = await validator.ValidateAsync(usuarioDto);

                if (!resultValidator.IsValid) return BadRequest(resultValidator);

                var data = await service.CreateUsuario(usuarioDto);

                return Ok(data);

            } catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = $"Error interno del servidor: {ex.Message} - {ex.InnerException}" });
            }
        }

    }
}
