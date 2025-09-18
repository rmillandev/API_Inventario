using API_Inventario.Utils.Objects;

namespace API_Inventario.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<PagedResult<T>> GetAll(int? pageNumber, int? pageSize);
        Task<T?> GetById(int id);
        Task<CreateSuccessResponse<T>> Create(T data);
        Task Update(int id, T data);
        Task Delete(int id);
    }
}
