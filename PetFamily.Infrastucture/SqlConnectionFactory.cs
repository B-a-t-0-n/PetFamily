using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace PetFamily.Infrastucture
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Create() =>
            new NpgsqlConnection(_configuration.GetConnectionString(Constants.DATABASE));
    }

}
