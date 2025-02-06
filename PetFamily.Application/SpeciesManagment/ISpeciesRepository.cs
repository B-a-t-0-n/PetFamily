using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Application.Species
{
    public interface ISpeciesRepository
    {
        Task<Guid> Add(Domain.SpeciesMenegment.Entity.Species species, CancellationToken cancellationToken = default);
        Task<Result<Domain.SpeciesMenegment.Entity.Species, Error>> GetById(SpeciesId id, CancellationToken cancellationToken = default);
        Guid Save(Domain.SpeciesMenegment.Entity.Species species, CancellationToken cancellationToken = default);
        Guid Delete(Domain.SpeciesMenegment.Entity.Species species, CancellationToken cancellationToken = default);
    }
}
