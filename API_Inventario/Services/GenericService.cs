using API_Inventario.Repositorys;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Services
{
    public abstract class GenericService<T> : IGenericService<T> where T : class
    {

        private readonly IGenericRepository<T> repository;

        protected GenericService(IGenericRepository<T> repository) 
        { 
            this.repository = repository;
        }

        public async Task<PagedResult<T>> GetAll(int? pageNumber, int? pageSize)
        {
            return await repository.GetAll(pageNumber, pageSize);
        }

        public async Task<T?> GetById(int id) => await repository.GetById(id);

        public virtual async Task<CreateSuccessResponse<T>> Create(T data)
        {
             return await repository.Create(data);
        }

        public virtual async Task Update(int id, T data)
        {
            await repository.Update(id, data);
        }

        public async Task Delete(int id)
        {
            await repository.Delete(id);
        }
    }
}
