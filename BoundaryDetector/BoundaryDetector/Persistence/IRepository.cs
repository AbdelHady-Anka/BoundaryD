using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoundaryDetector.Persistence
{
    public interface IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>, new()
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> FindByIdAsync(TKey id);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
