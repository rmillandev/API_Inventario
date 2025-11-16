using API_Inventario.Dtos.MovimientoInventarioDtos;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Objects;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Inventario.Controllers
{
    [Route("api/movimiento-inventario")]
    [ApiController]
    [Authorize]
    public class MovimientoInventarioController : ControllerBase
    {

        private readonly IMovimientoInventarioService service;

        public MovimientoInventarioController(IMovimientoInventarioService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<CreateSuccessResponse<CreateMovimientoDto>>> CreateMovimientoInventario([FromBody] CreateMovimientoDto createMovimientoDto, [FromServices] IValidator<CreateMovimientoDto> validator)
        {
            try
            {
                var resultValidator = await validator.ValidateAsync(createMovimientoDto);

                if (!resultValidator.IsValid) return BadRequest(resultValidator);

                var res = await service.CreateMovimientoInventario(createMovimientoDto);

                if (!res.Success) return Conflict(res);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = $"Error interno del servidor: {ex.InnerException}" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<HistorialMovimientoInventarioDto>>> GetMovimientoInventarioHistory([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            try
            {
                var data = await service.GetMovimientoInventarioHistory(pageNumber, pageSize);
                return Ok(data);
            } catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = $"Error interno del servidor: {ex.InnerException}" });
            }
        }

    }
}
