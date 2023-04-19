using Common.SqlRepository.Entities;
using System;
using System.Threading.Tasks;

namespace Common.SqlRepository
{
    internal class Sql : ISql
    {
        private readonly string _connectionString;

        public Sql(string sqlConnectionString)
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

        private CourseDbContext NewELIQSQLContext()
        {
            return new CourseDbContext(_connectionString);
        }
    }
}
