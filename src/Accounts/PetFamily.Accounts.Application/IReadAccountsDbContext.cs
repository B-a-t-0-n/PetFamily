using PetFamily.Accounts.Domain;

namespace PetFamily.Accounts.Application;

public interface IReadAccountsDbContext
{
    public IQueryable<User> Users { get; }
}
