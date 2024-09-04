using CSharpFunctionalExtensions;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Infrastucture.Repositories
{
    public interface IVolunteerRepository
    {
        Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Result<Volunteer>> GetById(VolunteerId id);
    }
}