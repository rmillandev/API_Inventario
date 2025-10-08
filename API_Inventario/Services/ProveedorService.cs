using API_Inventario.Dtos.ProveedorDTO;
using API_Inventario.Dtos.ProveedorDtos;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils;
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

        public async Task<PagedResult<ReadProveedorDTO>> GetAllDto(int? pageNumber, int? pageSize)
        {
            var query = repository.GetAllQuery()
                .Select(p => new ReadProveedorDTO()
                {
                    Nit = p.Nit,
                    Nombre = p.Nombre,
                    Telefono = p.Telefono,
                    Email = p.Email,
                    Direccion = string.IsNullOrEmpty(p.Direccion) ? "N/A" : p.Direccion
                });
            return await query.ToPagedResultAsync(pageNumber, pageSize);
        }

        public async Task<CreateSuccessResponse<CreateProveedorDTO>> CreateProveedor(CreateProveedorDTO proveedorDto)
        {
            var exist = await ExistsByNit(proveedorDto.Nit);
            if (exist)
            {
                return new CreateSuccessResponse<CreateProveedorDTO>
                { 
                    Success = false,
                    Message = "No se creo el proveedor porque el nit ya se encuentra registrado."
                };
            }

            Proveedor proveedor = new Proveedor() 
            { 
                Nit = proveedorDto.Nit,
                Nombre = proveedorDto.Nombre,
                Telefono = proveedorDto.Telefono,
                Direccion = proveedorDto.Direccion,
                Email = proveedorDto.Email
            };

            var data = await repository.Create(proveedor);

            return new CreateSuccessResponse<CreateProveedorDTO> 
            { 
                Success = true,
                Message = "Proveedor creado exitosamente.",
                Data = proveedorDto
            };

        }

        public async Task UpdateProveedor(int id,  UpdateProveedorDTO proveedorDto)
        {
            if (!string.IsNullOrEmpty(proveedorDto.Nit))
            {
                var exist = await ExistsByNit(proveedorDto.Nit);
                if (exist) throw new InvalidOperationException("El nit ya se encuentra registrado.");
            }

            var proveedor = await repository.GetById(id);
            if (proveedor == null) throw new KeyNotFoundException("Proveedor no encontrado.");

            proveedor.Nit = proveedorDto.Nit ?? proveedor.Nit;
            proveedor.Nombre = proveedorDto.Nombre ?? proveedor.Nombre;
            proveedor.Telefono = proveedorDto.Telefono ?? proveedor.Telefono;
            proveedor.Email = proveedorDto.Email ?? proveedor.Email;
            proveedor.Direccion = proveedorDto.Direccion ?? proveedor.Direccion;

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
