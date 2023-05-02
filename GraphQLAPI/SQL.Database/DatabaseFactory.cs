using SQL.Database.Entities;

namespace SQL.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly string _connectionString;

        public DatabaseFactory(string sqlConnectionString)
        {
            _connectionString = sqlConnectionString ?? throw new ArgumentNullException(nameof(sqlConnectionString));
        }

        public async Task<TEntity> AddItemAsync<TEntity>(TEntity entity)
        {
            using var db = GetNewDbContext();
            try
            {
                await db.AddAsync(entity);
                await db.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                throw new Exception("Something went wrong while executing sql query.", e);
            }

            return entity;
        }

        public async Task<TEntity> DeleteItemAsync<TEntity>(TEntity entity)
        {
            using var db = GetNewDbContext();
            try
            {
                db.Remove(entity);
                await db.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                throw new Exception("Something went wrong while executing sql query.", e);
            }

            return entity;
        }

        public TEntity Execute<TEntity>(Func<CourseDbContext, TEntity> func)
        {
            using var db = GetNewDbContext();
            try
            {
                return func(db);
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                throw new Exception("Something went wrong while executing sql query.", e);
            }
        }

        public async Task<TEntity> ExecuteAsync<TEntity>(Func<CourseDbContext, Task<TEntity>> func)
        {
            using var db = GetNewDbContext();
            try
            {
                return await func(db).ConfigureAwait(false);
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                throw new Exception("Something went wrong while executing sql query.", e);
            }
        }

        public async Task<TEntity> UpdateItemAsync<TEntity>(TEntity entity)
        {
            using var db = GetNewDbContext();
            try
            {
                db.Update(entity);
                await db.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                throw new Exception("Something went wrong while executing sql query.", e);
            }

            return entity;
        }

        public CourseDbContext GetCourseDbContext()
        {
            return GetNewDbContext();
        }

        private CourseDbContext GetNewDbContext()
        {
            return new CourseDbContext(_connectionString);
        }
    }
}
