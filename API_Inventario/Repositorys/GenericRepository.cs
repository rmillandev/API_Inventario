using API_Inventario.Db;
using API_Inventario.Repositorys.Interfaces;
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


        public async Task<PagedResult<T>> GetAll(int? pageNumber, int? pageSize)
        {
            var query = entities.AsQueryable();

            int _pageNumber = pageNumber ?? 1;
            int _pageSize = pageSize ?? 6;

            int totalRecords = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)_pageSize);

            var items = await query
                    .OrderBy(e => EF.Property<int>(e, "Id"))
                    .Skip((_pageNumber - 1) * _pageSize)
                    .Take(_pageSize)
                    .ToListAsync();

            return new PagedResult<T>
            {
                Data = items,
                PageNumber = _pageNumber,
                PageSize = _pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
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
