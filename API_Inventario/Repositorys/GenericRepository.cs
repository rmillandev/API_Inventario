using API_Inventario.Db;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Utils;
using API_Inventario.Utils.Objects;
using Microsoft.EntityFrameworkCore;

namespace API_Inventario.Repositorys
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly Context context;
        protected DbSet<T> entities => context.Set<T>();

        protected GenericRepository(Context context)
        {
            this.context = context;
        }

        public IQueryable<T> GetAllQuery()
        {
            return entities.AsQueryable();
        }

        public async Task<PagedResult<T>> GetAll(int? pageNumber, int? pageSize)
        {
            return await entities.OrderBy(e => EF.Property<int>(e, "Id")).ToPagedResultAsync(pageNumber, pageSize);
        }

        public async Task<T?> GetById(int id) => await entities.FindAsync(id);

        public async Task<CreateSuccessResponse<T>> Create(T data)
        {
            await entities.AddAsync(data);
            await context.SaveChangesAsync();
            return new CreateSuccessResponse<T>
            {
                Success = true,
                Message = "Registro creado exitosamente."
            };
        }

        public async Task Update(int id, T data)
        {
            var item = await entities.FindAsync(id);

            if (item != null)
            {
                context.Entry(item).CurrentValues.SetValues(data);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var item = await entities.FindAsync(id);

            if (item != null)
            {
                entities.Remove(item);
                await context.SaveChangesAsync();
            }
        }
    }
}
