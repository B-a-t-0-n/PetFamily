using PetFamily.Accounts.Domain;

namespace PetFamily.Accounts.Application.Managers;

public interface IVolunteerAccountManager
{
    Task CreateVolunteerAccount(VolunteerAccount volunteerAccount);
}