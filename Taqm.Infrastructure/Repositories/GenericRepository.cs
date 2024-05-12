using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using Taqm.Data.Consts;
using Taqm.Infrastructure.Abstracts;
using Taqm.Infrastructure.Data;

namespace Taqm.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Fields
        private readonly AppDbContext _context;
        #endregion

        #region Constructors
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public virtual async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<IEnumerable<T>> CreateRangeAsync(ICollection<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return entities;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateRangeAsync(ICollection<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(ICollection<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

        public virtual async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public virtual async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

        public virtual async Task RollBackAsync() => await _context.Database.RollbackTransactionAsync();

        public virtual async Task CommitAsync() => await _context.Database.CommitTransactionAsync();

        public virtual IQueryable<T> GetTableAsTracking() => _context.Set<T>().AsQueryable();

        public virtual IQueryable<T> GetTableNoTracking() => _context.Set<T>().AsNoTracking().AsQueryable();

        public virtual async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes) query = query.Include(include);
            }
            return await query.SingleOrDefaultAsync(criteria);
        }

        public virtual IQueryable<T> FindAllAsync(IQueryable<T> query, Expression<Func<T, bool>> filter,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            query = query.Where(filter);
            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query;
        }
        #endregion

    }
}
