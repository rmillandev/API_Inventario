using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Exceptions;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Services
{
    public class ProductoService : GenericService<Producto>, IProductoService
    {
        private readonly IProductoRepository repository;
        public ProductoService(IProductoRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        private async Task<bool> ExistsByCodigo(int codigo)
        {
            return await repository.ExistsByCodigo(codigo);
        }

        public override async Task<CreateSuccessResponse<Producto>> Create(Producto producto)
        {

            var existeCodigo = await ExistsByCodigo(producto.Codigo);

            if (existeCodigo) throw new BusinessException("El codigo ya se encuentra registrado en otro producto. Por favor verifique la informacion.");

            var res = await repository.Create(producto);

            return res;

        }

    }
}
