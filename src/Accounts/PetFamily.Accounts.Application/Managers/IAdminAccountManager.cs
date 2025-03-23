using PetFamily.Accounts.Domain;

namespace PetFamily.Accounts.Application.Managers;

public interface IAdminAccountManager
{
    Task CreateAdminAccount(AdminAccount adminAccount);
}