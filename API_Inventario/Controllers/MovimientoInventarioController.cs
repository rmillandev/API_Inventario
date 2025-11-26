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
        public async Task<ActionResult<CreateSuccessResponse<CreateMovimientoDto>>> CreateMovimientoInventario([FromBody] CreateMovimientoDto createMovimientoDto)
        {
            var res = await service.CreateMovimientoInventario(createMovimientoDto);
            return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<HistorialMovimientoInventarioDto>>> GetMovimientoInventarioHistory([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var data = await service.GetMovimientoInventarioHistory(pageNumber, pageSize);
            return Ok(data);
        }

    }
}
