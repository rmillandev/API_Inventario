using API_Inventario.Utils.Objects;
using Microsoft.EntityFrameworkCore;

namespace API_Inventario.Utils
{
    public static class IQueryableExtensions
    {
        // metodo statico que parezca que pertenece a la clase original this IQueryable, ej; entities.ToPagedResultAsync()
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int? pageNumber, int? pageSize)
        {
            int currentPage = pageNumber ?? 1;
            int currentSize = pageSize ?? 10;

            int totalRecords = await query.CountAsync();

            var data = await query
                .Skip((currentPage - 1) * currentSize)
                .Take(currentSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Data = data,
                PageNumber = currentPage,
                PageSize = currentSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)currentSize)
            };
        }
    }
}
