using API_Inventario.Dtos;
using API_Inventario.Dtos.ProductoDtos;
using API_Inventario.Models;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Exceptions;
using API_Inventario.Utils.Objects;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API_Inventario.Controllers
{
    [Route("api/producto")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {

        private readonly IProductoService service;

        public ProductoController(IProductoService service) {
            this.service = service;
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadProductoDTO>>> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] int? categoriaId, [FromQuery] int? proveedorId)
        {
            var data = await service.GetAllDto(pageNumber, pageSize, categoriaId, proveedorId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<CreateSuccessResponse<Producto>>> CreateProducto([FromBody] CreateProductoDTO productoDto)
        {
            var resCreate = await service.CreateProducto(productoDto);
            return Ok(resCreate);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateProducto([FromRoute] int id, [FromBody] UpdateProductoDTO updateProductoDto)
        {
            await service.UpdateProducto(id, updateProductoDto);
            return NoContent();
        }

        [HttpDelete]
        [Route("{codigoProducto}")]
        public async Task<ActionResult> DeleteByCodigoProducto([FromRoute] int codigoProducto)
        {
            await service.DeleteByCodigoProducto(codigoProducto);
            return NoContent();
        }

        [HttpGet]
        [Route("stock-bajo")]
        public async Task<ActionResult<PagedResult<ReadLowStockProductoDto>>> GetLowStockProducts([FromQuery] int? pageNumber, [FromQuery] int? pageSize)

        {
            var data = await service.GetLowStockProducts(pageNumber, pageSize);
            return Ok(data);
        }

    }
}
