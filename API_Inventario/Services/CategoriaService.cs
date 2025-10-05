using API_Inventario.Dtos.CategoriaDtos;
using API_Inventario.Models;
using API_Inventario.Repositorys;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Services
{
    public class CategoriaService : GenericService<Categoria>, ICategoriaService
    {
        private readonly ICategoriaRepository repository;
        public CategoriaService(ICategoriaRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public async Task<CreateSuccessResponse<CreateCategoriaDTO>> CreateCategoria(CreateCategoriaDTO categoriaDto) 
        {
            bool exists = await repository.ExistsByName(categoriaDto.Nombre);

            if (exists)
            {
                return new CreateSuccessResponse<CreateCategoriaDTO>
                { 
                    Success = false,
                    Message = "No se creo el registro. Este nombre de categoria ya se encuentra registrado."
                };
            }

            Categoria categoria = new Categoria()
            {
                Nombre = categoriaDto.Nombre,
                Descripcion = categoriaDto.Descripcion,
            };

            var data = await repository.Create(categoria);

            return new CreateSuccessResponse<CreateCategoriaDTO>
            {
                Success = true,
                Message = "Categoria creada correctamente.",
                Data = categoriaDto
            };
        }

        public async Task UpdateCategoria(int id, UpdateCategoriaDTO categoriaDto)
        {

            var categoria = await repository.GetById(id);

            if (categoria == null) throw new KeyNotFoundException("Este ID no existe.");

            if (!string.IsNullOrEmpty(categoriaDto.Nombre))
            {
                bool exists = await repository.ExistsByName(categoriaDto.Nombre);

                if (exists) throw new InvalidOperationException("El nombre de la categoria ya existe.");
            }

            categoria.Nombre = categoriaDto.Nombre ?? categoria.Nombre;          
            categoria.Descripcion = categoriaDto.Descripcion ?? categoria.Descripcion;

            await repository.Update(id, categoria);

        }

    }
}
