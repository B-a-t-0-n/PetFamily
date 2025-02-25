using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Contracts;

namespace PetFamily.Volunteers.Presentation;

public class VolunteersContract : IVolunteersContract
{
    private readonly IReadVolunteersDbContext _readDbContext;

    public VolunteersContract(IReadVolunteersDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<UnitResult<ErrorList>> DoesAnyPetHaveBreedWithId(Guid speciesId, Guid breedId)
    {
        if (await _readDbContext.Pets.AnyAsync(p => p.SpeciesId == speciesId && p.BreedId == breedId))
            return Result.Success<ErrorList>();

        return Errors.General.NotFound().ToErrorList();
    }

    public async Task<UnitResult<ErrorList>> DoesAnyPetHaveSpeciesWithId(Guid speciesId)
    {
        if(await _readDbContext.Pets.AnyAsync(p => p.SpeciesId == speciesId))
            return Result.Success<ErrorList>();

        return Errors.General.NotFound().ToErrorList();
    }
}
