using PetFamily.Accounts.Domain;

namespace PetFamily.Accounts.Application.Managers;

public interface IPartisipantAccountManager
{
    Task CreatePartisipantAccount(PartisipantAccount partisipantAccount);
}