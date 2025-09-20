using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Data.Context
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }

    public sealed class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Default")
                ?? throw new InvalidOperationException("Falta ConnectionStrings:Default en appsettings.json");
        }

        public IDbConnection Create()
        {
            return new SqlConnection(_connectionString);
        }
    }
}


