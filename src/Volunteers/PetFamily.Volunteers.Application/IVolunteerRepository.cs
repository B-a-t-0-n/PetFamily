using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Volunteers.Domain.Entity;


namespace PetFamily.Volunteers.Application;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetById(VolunteerId id, CancellationToken cancellationToken = default);
    Guid Save(Volunteer volunteer, CancellationToken cancellationToken = default);
    Guid Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
}