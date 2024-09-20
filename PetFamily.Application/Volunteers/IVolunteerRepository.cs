using CSharpFunctionalExtensions;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Infrastucture.Repositories
{
    public interface IVolunteerRepository
    {
        Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Result<Volunteer, Error>> GetById(VolunteerId id, CancellationToken cancellationToken = default);
        Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Guid> Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
    }
}