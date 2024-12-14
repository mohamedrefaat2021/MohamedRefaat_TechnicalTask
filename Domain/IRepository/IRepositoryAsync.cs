using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetAsync(int id, bool allowTracking = false);
        Task<IEnumerable<T>> GetAllPagedListAsync(Expression<Func<T, bool>> filter, string includeProperties = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );

        Task<PagedListResponse<T>> GetAllPagedListAsync(Expression<Func<T, bool>> filter, int pageNumber, int pageSize, string includeProperties = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            , bool allowTracking = false
            );

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);
        Task RemoveAsync(int id);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entity);
        void Update(T entity);

        void UpdateRange(IEnumerable<T> entity);

        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

    }
}
