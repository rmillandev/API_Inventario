using System.ComponentModel.DataAnnotations;
using API_Inventario.Dtos.ProveedorDTO;
using API_Inventario.Dtos.ProveedorDtos;
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
        public async Task<ActionResult<PagedResult<ReadProveedorDTO>>> GetAllDto([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            try
            {
                var data = await service.GetAllDto(pageNumber, pageSize);
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
        public async Task<ActionResult<CreateSuccessResponse<CreateProveedorDTO>>> CreateProveedor([FromBody] CreateProveedorDTO proveedorDto, [FromServices] IValidator<CreateProveedorDTO> validator)
        {
            try
            {
                var resultValditor = await validator.ValidateAsync(proveedorDto);

                if (!resultValditor.IsValid) return BadRequest(resultValditor);

                var data = await service.CreateProveedor(proveedorDto);

                if (!data.Success) return Conflict(new { errorMessage = data.Message });

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateProveedor([FromRoute] int id, [FromBody] UpdateProveedorDTO proveedorDto, [FromServices] IValidator<UpdateProveedorDTO> validator)
        {
            try
            {

                var resultValditor = await validator.ValidateAsync(proveedorDto);

                if (!resultValditor.IsValid) return BadRequest(resultValditor);

                await service.UpdateProveedor(id, proveedorDto);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { errorMessage = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = $"Error interno del servidor: {ex.Message}" });
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
