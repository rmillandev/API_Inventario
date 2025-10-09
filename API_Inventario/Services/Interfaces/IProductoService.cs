using API_Inventario.Dtos.ProductoDtos;
using API_Inventario.Models;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Services.Interfaces
{
    public interface IProductoService : IGenericService<Producto>
    {
        Task<PagedResult<ReadProductoDTO>> GetAllDto(int? pageNumber, int? pageSize, int? categoriaId, int? proveedorId);
        Task DeleteByCodigoProducto(int codigo);
        Task UpdateProducto(int codigo, UpdateProductoDTO updateProductoDto);
        Task<PagedResult<ReadLowStockProductoDto>> GetLowStockProducts(int? pageNumber, int? pageSize);
    }
}
