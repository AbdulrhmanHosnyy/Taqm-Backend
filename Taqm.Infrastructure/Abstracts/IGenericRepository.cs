using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using Taqm.Data.Consts;

namespace Taqm.Infrastructure.Abstracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(ICollection<T> entities);
        Task<T> GetByIdAsync(int id);
        Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task RollBackAsync();
        Task CommitAsync();
        IQueryable<T> GetTableAsTracking();
        IQueryable<T> GetTableNoTracking();
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> criteria,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);

    }
}
