using Common.SqlRepository.Entities;
using System;
using System.Threading.Tasks;

namespace Common.SqlRepository
{
    public class SqlFactory : ISqlFactory
    {
        private readonly string _connectionString;

        public SqlFactory(string sqlConnectionString)
        {
            _connectionString = sqlConnectionString ?? throw new ArgumentNullException(nameof(sqlConnectionString));
        }

        public async Task<T> ExecuteAsync<T>(Func<CourseDbContext, Task<T>> func)
        {
            using var db = NewELIQSQLContext();
            try
            {
                return await func(db).ConfigureAwait(false);
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                throw new Exception("Something went wrong while executing sql query.", e);
            }
        }

        public T Execute<T>(Func<CourseDbContext, T> func)
        {
            using (var db = NewELIQSQLContext())
            {
                try
                {
                    return func(db);
                }
                catch (Microsoft.Data.SqlClient.SqlException e)
                {
                    throw new Exception("Something went wrong while executing sql query.", e);
                }
            }
        }

        public async Task<T> AddItemAsync<T>(T entity)
        {
            using (var db = NewELIQSQLContext())
            {
                try
                {
                    await db.AddAsync(entity);
                    await db.SaveChangesAsync();
                }
                catch (Microsoft.Data.SqlClient.SqlException e)
                {
                    throw new Exception("Something went wrong while executing sql query.", e);
                }
            }

            return entity;
        }

        public async Task<TEntity> UpdateItemAsync<TEntity>(TEntity entity)
        {
            using (var db = NewELIQSQLContext())
            {
                try
                {
                    db.Update(entity);
                    await db.SaveChangesAsync();
                }
                catch (Microsoft.Data.SqlClient.SqlException e)
                {
                    throw new Exception("Something went wrong while executing sql query.", e);
                }
            }

            return entity;
        }

        public async Task<TEntity> DeleteItemAsync<TEntity>(TEntity entity)
        {
            using (var db = NewELIQSQLContext())
            {
                try
                {
                    db.Remove(entity);
                    await db.SaveChangesAsync();
                }
                catch (Microsoft.Data.SqlClient.SqlException e)
                {
                    throw new Exception("Something went wrong while executing sql query.", e);
                }
            }

            return entity;
        }

        private CourseDbContext NewELIQSQLContext()
        {
            return new CourseDbContext(_connectionString);
        }
    }
}
