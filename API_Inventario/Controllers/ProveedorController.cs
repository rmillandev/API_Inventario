using System.ComponentModel.DataAnnotations;
using API_Inventario.Dtos;
using API_Inventario.Models;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Objects;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Inventario.Controllers
{
    [Route("api/proveedor")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {

        private readonly IProveedorService service;

        public ProveedorController(IProveedorService service) 
        {
            this.service = service;
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<Proveedor>>> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            try
            {
                var data = await service.GetAll(pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception ex) {
                return StatusCode(500, new 
                {
                    errorMessage = $"Error interno del servidor: {ex.Message}"
                });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var proveedor = await service.GetById(id);

                if (proveedor == null) return NotFound();

                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    errorMessage = $"Error interno del servidor: {ex.Message}"
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateSuccessResponse<Proveedor>>> Create([FromBody] Proveedor proveedor, [FromServices] IValidator<Proveedor> validator)
        {
            try
            {
                var resultValditor = await validator.ValidateAsync(proveedor);

                if (!resultValditor.IsValid) return BadRequest(resultValditor);

                var data = await service.Create(proveedor);

                return Ok(data);
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

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ProveedorDTO proveedor, [FromServices] IValidator<ProveedorDTO> validator)
        {
            try
            {
                var exist = await service.GetById(id);

                if (exist == null) return NotFound();
                var resultValditor = await validator.ValidateAsync(proveedor);

                if (!resultValditor.IsValid) return BadRequest(resultValditor);

                await service.UpdateProveedor(id, proveedor);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    errorMessage = ex.Message,
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
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
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var proveedor = await service.GetById(id);

                if (proveedor == null) return NotFound();

                await service.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    errorMessage = $"Error interno del servidor: {ex.Message}"
                });
            }
        }
    }
}
