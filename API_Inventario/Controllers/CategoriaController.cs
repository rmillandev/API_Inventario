using API_Inventario.Dtos.CategoriaDtos;
using API_Inventario.Models;
using API_Inventario.Services;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Objects;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API_Inventario.Controllers
{
    [Route("api/categoria")]
    [ApiController]
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
            try
            {
                var data = await service.GetAll(pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var categoria = await service.GetById(id);

                if (categoria == null) return NotFound();

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateSuccessResponse<CreateCategoriaDTO>>> Create(
            [FromBody] CreateCategoriaDTO categoriaDto, 
            [FromServices] IValidator<CreateCategoriaDTO> validator
        )
        {
            try
            {
                var result = await validator.ValidateAsync(categoriaDto);

                if (!result.IsValid) return BadRequest(result);

                var data = await service.CreateCategoria(categoriaDto);

                if (!data.Success) return StatusCode(409, new { message = data.Message }); 

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    errorMessage = $"Error interno del servidor: {ex.Message}"
                });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoriaDTO categoriaDto)
        {
            try
            {
                await service.UpdateCategoria(id, categoriaDto);
                return NoContent();
            } 
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    errorMessage = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    errorMessage = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new 
                {
                    errorMessage = $"Error interno del servidor: {ex.Message}"
                });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var item = await service.GetById(id);

                if (item == null) return NotFound();

                await service.Delete(id);

                return NoContent();

            } catch (Exception ex)
            {
                return StatusCode(500, new 
                { 
                    errorMessage = $"Error interno del servidor: {ex.Message}"
                });
            }
        }

    }
}
