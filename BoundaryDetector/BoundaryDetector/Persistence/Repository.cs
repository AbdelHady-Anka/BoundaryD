using BoundaryDetector.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoundaryDetector.Persistence
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>, new()
    {
        private SQLiteAsyncConnection _connection;
        public Repository(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _ = _connection.CreateTableAsync<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _connection.InsertAsync(entity);
        }

        public async Task Delete(TEntity entity)
        {
            await _connection.DeleteAsync(entity);
        }

        public async Task<TEntity> FindByIdAsync(TKey id)
        {
            var result = await _connection.FindAsync<TEntity>(id);

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            
            var result =  await _connection.Table<TEntity>().Where(expression).ToListAsync().ConfigureAwait(false);

            return result;
        }

        public async Task Update(TEntity entity)
        {
            await _connection.UpdateAsync(entity);
        }
    }
}
