using API_Inventario.Dtos;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Services
{
    public class ProveedorService : GenericService<Proveedor>, IProveedorService
    {
        private readonly IProveedorRepository repository;

        public ProveedorService(IProveedorRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public override async Task<CreateSuccessResponse<Proveedor>> Create(Proveedor proveedor)
        {
            var exist = await ExistsByNit(proveedor.Nit);
            if (exist) throw new InvalidOperationException("El nit ya se encuentra registrado.");

            var data = await repository.Create(proveedor);

            return data;

        }

        public async Task UpdateProveedor(int id,  ProveedorDTO proveedorDTO)
        {
            if (!string.IsNullOrEmpty(proveedorDTO.Nit))
            {
                var exist = await ExistsByNit(proveedorDTO.Nit);
                if (exist) throw new InvalidOperationException("El nit ya se encuentra registrado.");
            }

            var proveedor = await repository.GetById(id);
            if (proveedor == null) throw new KeyNotFoundException("Proveedor no encontrado.");

            if (!string.IsNullOrEmpty(proveedorDTO.Nit))
                proveedor.Nit = proveedorDTO.Nit;

            if (!string.IsNullOrEmpty(proveedorDTO.Nombre))
                proveedor.Nombre = proveedorDTO.Nombre;

            if (!string.IsNullOrEmpty(proveedorDTO.Telefono))
                proveedor.Telefono = proveedorDTO.Telefono;

            if (!string.IsNullOrEmpty(proveedorDTO.Email))
                proveedor.Email = proveedorDTO.Email;

            if (!string.IsNullOrEmpty(proveedorDTO.Direccion))
                proveedor.Direccion = proveedorDTO.Direccion;

            await repository.Update(id, proveedor);
        }

        private async Task<bool> ExistsByNit(string nit)
        {
            string nitSinEspacios = nit.Trim();
            bool exist = await repository.ExistsByNit(nitSinEspacios);
            return exist;
        }

    }
}
