using Microsoft.Extensions.Configuration;
using Npgsql;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel;
using System.Data;

namespace PetFamily.Species.Infrastructure;

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
