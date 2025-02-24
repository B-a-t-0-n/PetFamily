using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.IDs;

namespace PetFamily.Species.Application;

public interface ISpeciesRepository
{
    Task<Guid> Add(Domain.Entity.Species species, CancellationToken cancellationToken = default);
    Task<Result<Domain.Entity.Species, Error>> GetById(SpeciesId id, CancellationToken cancellationToken = default);
    Guid Save(Domain.Entity.Species species, CancellationToken cancellationToken = default);
    Guid Delete(Domain.Entity.Species species, CancellationToken cancellationToken = default);
}
