using API_Inventario.Dtos.ProductoDtos;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils;
using API_Inventario.Utils.Constanst;
using API_Inventario.Utils.Exceptions;
using API_Inventario.Utils.Objects;
using AutoMapper;

namespace API_Inventario.Services
{
    public class ProductoService : GenericService<Producto>, IProductoService
    {
        private readonly IProductoRepository repository;
        private readonly IMapper mapper;
        public ProductoService(IProductoRepository repository, IMapper mapper) : base(repository)
        {
            this.repository = repository; 
            this.mapper = mapper;
        }

        private async Task<bool> ExistsByCodigo(int codigo)
        {
            return await repository.ExistsByCodigo(codigo);
        }

        public async Task<PagedResult<ReadProductoDTO>> GetAllDto(int? pageNumber, int? pageSize, int? categoriaId, int? proveedorId)
        {
            var query = repository.GetAllQuery();

            if (categoriaId.HasValue) query = query.Where(p => p.CategoriaId == categoriaId);

            if (proveedorId.HasValue) query = query.Where(p => p.ProveedorId == proveedorId);

            var queryDto = query
                .Where(p => p.Activo)
                .Select(p => new ReadProductoDTO
                {
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    StockActual = p.StockActual,
                    Categoria = p.Categoria.Nombre,
                    Proveedor = p.Proveedor.Nombre
                });
            return await queryDto.ToPagedResultAsync(pageNumber, pageSize);
        }

        public override async Task<CreateSuccessResponse<Producto>> Create(Producto producto)
        {

            var existeCodigo = await ExistsByCodigo(producto.Codigo);

            if (existeCodigo) throw new BusinessException("El codigo ya se encuentra registrado en otro producto. Por favor verifique la informacion.");

            if (producto.StockActual < producto.StockMinimo) throw new BusinessException("El StockActual no puede ser menor que el StockMinimo.");

            var res = await repository.Create(producto);

            return res;

        }

        public async Task DeleteByCodigoProducto(int codigo)
        {
            await repository.DeleteByCodigoProducto(codigo);
        }

        public async Task UpdateProducto(int id, UpdateProductoDTO updateProductoDto)
        {
          
            var producto = await repository.GetById(id);

            if (producto == null) throw new KeyNotFoundException("El ID no fue encontrado.");

            producto.Nombre = updateProductoDto.Nombre ?? producto.Nombre;
            producto.Descripcion = updateProductoDto.Descripcion ?? producto.Descripcion;
            producto.Precio = updateProductoDto.Precio ?? producto.Precio;
            producto.StockActual = updateProductoDto.StockActual ?? producto.StockActual;
            producto.CategoriaId = updateProductoDto.CategoriaId ?? producto.CategoriaId;
            producto.ProveedorId = updateProductoDto.ProveedorId ?? producto.ProveedorId;

            await repository.Update(id, producto);

        }

        public async Task<PagedResult<ReadLowStockProductoDto>> GetLowStockProducts(int? pageNumber, int? pageSize)
        { 
            var query = repository.GetAllQuery()
                .Where(p => p.StockActual < p.StockMinimo)
                .Select(p => new ReadLowStockProductoDto
                {
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    StockActual = p.StockActual,
                    Categoria = p.Categoria.Nombre,
                    Proveedor = p.Proveedor.Nombre,
                    StockBajo = (p.StockActual == 0 || !p.Activo) ? "No hay unidades disponibles" : "Quedan pocas unidades"
                });
            return await query.ToPagedResultAsync(pageNumber, pageSize);
        }

        public async Task UpdateStockProduct(int id, int cantidad, string tipoMovimiento)
        {
            var producto = await repository.GetById(id);

            if (producto == null) throw new KeyNotFoundException("El producto no existe.");

            if (tipoMovimiento == ConstantsMovimientoInventario.TIPO_MOVIMIENTO_ENTRADA)
            {
                producto.StockActual += cantidad;
            } else if (tipoMovimiento == ConstantsMovimientoInventario.TIPO_MOVIMIENTO_SALIDA)
            {
                if (producto.StockActual < cantidad) throw new BusinessException("Stock insuficiente para realizar la salida.");

                producto.StockActual -= cantidad;
            }

            producto.Activo = producto.StockActual > 0;

            await repository.Update(id, producto);
        }

    }
}
