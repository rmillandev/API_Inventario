using API_Inventario.Dtos.CategoriaDtos;
using API_Inventario.Models;
using API_Inventario.Services;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Objects;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API_Inventario.Controllers
{
    [Route("api/categoria")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaService service;

        public CategoriaController(ICategoriaService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadCategoriaDTO>>> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var data = await service.GetAll(pageNumber, pageSize);
            return Ok(data);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var categoria = await service.GetById(id);
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CreateSuccessResponse<CreateCategoriaDTO>>> Create([FromBody] CreateCategoriaDTO categoriaDto)
        {
            var data = await service.CreateCategoria(categoriaDto);
            return Ok(data);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoriaDTO categoriaDto)
        {
            await service.UpdateCategoria(id, categoriaDto);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await service.Delete(id);
            return NoContent();
        }

    }
}
