using SQL.Database.Entities;

namespace SQL.Database
{
    public interface IDatabaseFactory
    {
        Task<TEntity> ExecuteAsync<TEntity>(Func<CourseDbContext, Task<TEntity>> func);
        TEntity Execute<TEntity>(Func<CourseDbContext, TEntity> func);
        Task<TEntity> AddItemAsync<TEntity>(TEntity entity);
        Task<TEntity> UpdateItemAsync<TEntity>(TEntity entity);
        Task<TEntity> DeleteItemAsync<TEntity>(TEntity entity);
    }
}
