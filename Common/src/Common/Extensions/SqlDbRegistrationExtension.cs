using Common.Models;
using Common.SqlRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Common.Extensions
{
    public static class SqlDbRegistrationExtension
    {
        public static IServiceCollection AddSqlDatabase(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                var sqlSettings = configuration?.GetSection(nameof(SqlSettings)).Get<SqlSettings>();
                if (sqlSettings == null || string.IsNullOrEmpty(sqlSettings.ConnectionString))
                    throw new ArgumentNullException(nameof(sqlSettings));

                var sql = new Sql(sqlSettings.ConnectionString);
                return sql;
            });

            return services;
        }
    }
}
