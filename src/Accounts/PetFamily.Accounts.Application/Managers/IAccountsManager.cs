using PetFamily.Accounts.Domain;

namespace PetFamily.Accounts.Application.Managers;

public interface IAccountsManager
{
    Task CreateAdminAccount(AdminAccount adminAccount, CancellationToken cancellationToken = default);
    Task CreatePartisipantAccount(PartisipantAccount partisipantAccount, CancellationToken cancellationToken = default);
    Task CreateVolunteerAccount(VolunteerAccount volunteerAccount, CancellationToken cancellationToken = default);
}