using System.Data;

namespace PetFamily.Infrastucture
{
    public interface ISqlConnectionFactory
    {
        IDbConnection Create();
    }
}