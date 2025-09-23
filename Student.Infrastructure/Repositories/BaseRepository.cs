using Microsoft.EntityFrameworkCore;
using Students.Domain.Generic;
using Students.Infrastructure.DBContext;
using System.Linq.Expressions;

namespace Students.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> _dbSet;
        public BaseRepository(StudentDatabaseContext context)
        {
            _dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            var result = await _dbSet.Where(expression).ToListAsync();
            if(result == null)
            {
                return;
            }
            _dbSet.RemoveRange(result);
        }

        public async Task<TEntity> FindByExpressionAsyn(Expression<Func<TEntity, bool>> expression)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(expression);
            return entity;
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(entity);
        }
    }
}
