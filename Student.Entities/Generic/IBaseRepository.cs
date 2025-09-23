using System.Linq.Expressions;

namespace Students.Domain.Generic
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> FindByExpressionAsyn(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
    }
}
