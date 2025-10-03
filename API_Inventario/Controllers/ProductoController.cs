using API_Inventario.Dtos;
using API_Inventario.Dtos.ProductoDtos;
using API_Inventario.Models;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Exceptions;
using API_Inventario.Utils.Objects;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace API_Inventario.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private readonly IProductoService service;

        public ProductoController(IProductoService service) {
            this.service = service;
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadProductoDTO>>> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            try
            {
                var data = await service.GetAllDto(pageNumber, pageSize);
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
        [Route("{codigo}")]
        public async Task<ActionResult> UpdateProducto([FromRoute] int codigo, [FromBody] UpdateProductoDTO updateProductoDto)
        {
            try
            {
                await service.UpdateProducto(codigo, updateProductoDto);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = $"Error interno del servidor: {ex.Message}" });
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

    }
}
