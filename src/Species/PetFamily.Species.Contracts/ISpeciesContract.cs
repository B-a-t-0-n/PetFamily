using CSharpFunctionalExtensions;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel;

namespace PetFamily.Species.Contracts;

public interface ISpeciesContract
{
    Task<Result<SpeciesDto, ErrorList>> GetSpeciesById(
        Guid speciesId,
        CancellationToken cancellationToken = default);

    Task<Result<BreedDto, ErrorList>> GetBreedById(
        Guid speciesId,
        Guid breedId,
        CancellationToken cancellationToken = default);
}
