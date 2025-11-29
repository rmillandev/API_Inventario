using API_Inventario.Dtos.UsuarioDtos;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Objects;
using API_Inventario.Validations.UsuarioValidation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API_Inventario.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        
        private readonly IUsuarioService service;

        public UsuarioController(IUsuarioService service)
        {
            this.service = service;
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadUsuarioDTO>>> GetAllDto([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var data = await service.GetAllDto(pageNumber, pageSize);
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<ShowSuccessCreateUserDTO>> CreateUsuario([FromBody] CreateUsuarioDTO usuarioDto)
        {
            var data = await service.CreateUsuario(usuarioDto);
            return Ok(data);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateUsuario([FromRoute] int id, [FromBody] UpdateUsuarioDTO usuarioDto)
        {
            await service.UpdateUsuario(id, usuarioDto);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteUsuario([FromRoute] int id)
        {
            await service.Delete(id);
            return NoContent();
        }
    }
}
