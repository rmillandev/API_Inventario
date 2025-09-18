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

        public override async Task<CreateSuccessResponse<Categoria>> Create(Categoria categoria) 
        {
            bool exists = await repository.ExistsByName(categoria.Nombre);

            if (exists)
            {
                return new CreateSuccessResponse<Categoria>
                { 
                    Success = false,
                    Message = "No se creo el registro. Este nombre de categoria ya se encuentra registrado."
                };
            }

            var data = await repository.Create(categoria);

            return data;
        }

        public override async Task Update(int id, Categoria categoria)
        {
  
            bool exists = await repository.ExistsByName(categoria.Nombre);

            if (exists) throw new InvalidOperationException("El nombre de la categoria ya existe.");

            await repository.Update(id, categoria);
            
        }

    }
}
