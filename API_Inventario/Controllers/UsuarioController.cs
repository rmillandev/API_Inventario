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
        public async Task<ActionResult<ShowSuccessCreateUserDTO>> CreateUsuario([FromBody] CreateUsuarioDTO usuarioDto)
        {
            var data = await service.CreateUsuario(usuarioDto);
            return Ok(data);
        }

    }
}
