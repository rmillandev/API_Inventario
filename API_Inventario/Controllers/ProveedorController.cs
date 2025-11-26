using System.ComponentModel.DataAnnotations;
using API_Inventario.Dtos.ProveedorDTO;
using API_Inventario.Dtos.ProveedorDtos;
using API_Inventario.Models;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Objects;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API_Inventario.Controllers
{
    [Route("api/proveedor")]
    [ApiController]
    [Authorize]
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
            var data = await service.GetAllDto(pageNumber, pageSize);
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<CreateSuccessResponse<CreateProveedorDTO>>> CreateProveedor([FromBody] CreateProveedorDTO proveedorDto)
        {
            var data = await service.CreateProveedor(proveedorDto);
            return Ok(data);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateProveedor([FromRoute] int id, [FromBody] UpdateProveedorDTO proveedorDto)
        {
            await service.UpdateProveedor(id, proveedorDto);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await service.Delete(id);
            return NoContent();
        }
    }
}
