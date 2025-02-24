using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel;
using PetFamily.Species.Application;
using PetFamily.Species.Contracts;

namespace PetFamily.Species.Presentation;

public class SpeciesContract : ISpeciesContract
{
    private readonly IReadSpeciesDbContext _readDbContext;

    public SpeciesContract(IReadSpeciesDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<BreedDto, ErrorList>> GetBreedById(Guid speciesId, Guid breedId, CancellationToken cancellationToken = default)
    {
        var breedDto = await _readDbContext.Breeds
            .FirstOrDefaultAsync(s => s.SpeciesId == speciesId && s.Id == breedId, cancellationToken);
        if (breedDto is null)
            return Errors.General.NotFound(breedId).ToErrorList();

        return breedDto;
    }

    public async Task<Result<SpeciesDto, ErrorList>> GetSpeciesById(Guid speciesId, CancellationToken cancellationToken = default)
    {
        var speciesDto = await _readDbContext.Species.FirstOrDefaultAsync(s => s.Id == speciesId, cancellationToken);
        if (speciesDto is null)
            return Errors.General.NotFound(speciesId).ToErrorList();

        return speciesDto;
    }
}
