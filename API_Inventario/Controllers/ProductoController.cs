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
            try
            {
                var data = await service.GetAllDto(pageNumber, pageSize, categoriaId, proveedorId);
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

        [HttpPost]
        public async Task<ActionResult<CreateSuccessResponse<Producto>>> Create([FromBody] CreateProductoDTO productoDto, [FromServices] IValidator<CreateProductoDTO> validator)
        {

            try
            {
                var resValidator = await validator.ValidateAsync(productoDto);

                if (!resValidator.IsValid) return BadRequest(resValidator);

                Producto producto = new Producto()
                {
                    Codigo = productoDto.Codigo,
                    Nombre = productoDto.Nombre,
                    Descripcion = productoDto.Descripcion,
                    Precio = productoDto.Precio,
                    StockActual = productoDto.StockActual,
                    StockMinimo = productoDto.StockMinimo,
                    Activo = productoDto.Activo,
                    CategoriaId = productoDto.CategoriaId,
                    ProveedorId = productoDto.ProveedorId
                };

                var resCreate = await service.Create(producto);

                return Ok(resCreate);

            }
            catch (BusinessException ex)
            {
                return Conflict(new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = $"Error interno del servidor: {ex.Message}" });
            }

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateProducto([FromRoute] int id, [FromBody] UpdateProductoDTO updateProductoDto)
        {
            try
            {
                await service.UpdateProducto(id, updateProductoDto);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = $"Error interno del servidor: {ex.Message} - {ex.InnerException}" });
            }
        }

        [HttpDelete]
        [Route("{codigoProducto}")]
        public async Task<ActionResult> DeleteByCodigoProducto([FromRoute] int codigoProducto)
        {
            try
            {
                await service.DeleteByCodigoProducto(codigoProducto);
                return NoContent();
            }
            catch (BusinessException ex)
            {
                return Conflict(new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("stock-bajo")]
        public async Task<ActionResult<PagedResult<ReadLowStockProductoDto>>> GetLowStockProducts([FromQuery] int? pageNumber, [FromQuery] int? pageSize)

        {
            try
            {
                var data = await service.GetLowStockProducts(pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = $"Error interno del servidor: {ex.Message}" });
            }
        }

    }
}
