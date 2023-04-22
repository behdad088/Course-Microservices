using Common.SqlRepository.Entities;
using System;
using System.Threading.Tasks;

namespace Common.SqlRepository
{
    public interface ISqlFactory
    {
        Task<T> ExecuteAsync<T>(Func<CourseDbContext, Task<T>> func);
        T Execute<T>(Func<CourseDbContext, T> func);
        Task<T> AddItemAsync<T>(T entity);
        Task<TEntity> UpdateItemAsync<TEntity>(TEntity entity);
        Task<TEntity> DeleteItemAsync<TEntity>(TEntity entity);
    }
}
