using Common.SqlRepository.Entities;
using System;
using System.Threading.Tasks;

namespace Common.SqlRepository
{
    public interface ISql
    {
        Task<T> ExecuteAsync<T>(Func<CourseDbContext, Task<T>> func);
        T Execute<T>(Func<CourseDbContext, T> func);
    }
}
