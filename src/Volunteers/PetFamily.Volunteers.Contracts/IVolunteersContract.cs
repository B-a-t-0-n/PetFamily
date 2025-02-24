using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Contracts;

public interface IVolunteersContract
{
    Task<UnitResult<ErrorList>> DoesAnyPetHaveSpeciesWithId(Guid speciesId);
    Task<UnitResult<ErrorList>> DoesAnyPetHaveBreedWithId(Guid speciesId, Guid breedId);
}
