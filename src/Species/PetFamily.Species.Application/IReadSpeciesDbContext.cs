using PetFamily.Core.Dtos;

namespace PetFamily.Species.Application;

public interface IReadSpeciesDbContext
{
    IQueryable<SpeciesDto> Species { get; }
    IQueryable<BreedDto> Breeds { get; }
}
