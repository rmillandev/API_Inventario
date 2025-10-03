using API_Inventario.Dtos.ProductoDtos;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils;
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

        public async Task<PagedResult<ReadProductoDTO>> GetAllDto(int? pageNumber, int? pageSize)
        {
            var query = repository.GetAllQuery()
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
            return await query.ToPagedResultAsync(pageNumber, pageSize);
        }

        public override async Task<CreateSuccessResponse<Producto>> Create(Producto producto)
        {

            var existeCodigo = await ExistsByCodigo(producto.Codigo);

            if (existeCodigo) throw new BusinessException("El codigo ya se encuentra registrado en otro producto. Por favor verifique la informacion.");

            var res = await repository.Create(producto);

            return res;

        }

        public async Task DeleteByCodigoProducto(int codigo)
        {
            await repository.DeleteByCodigoProducto(codigo);
        }

        public async Task UpdateProducto(int codigo, UpdateProductoDTO updateProductoDto)
        {
          
            var producto = await repository.GetByCodigo(codigo);

            if (producto == null) throw new KeyNotFoundException("El codigo de este producto no existe.");

            mapper.Map(updateProductoDto, producto);

            await repository.UpdateProducto(producto);

        }

    }
}
