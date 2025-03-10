using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ValueObjects.IDs;

namespace PetFamily.Volunteers.Domain.ValueObjects;

public class SpeciesAndBreed : ValueObject
{
    private SpeciesAndBreed() { }
    private SpeciesAndBreed(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }

    public SpeciesId SpeciesId { get; } = default!;
    public Guid BreedId { get; } = default!;

    public static Result<SpeciesAndBreed> Create(SpeciesId speciesId, Guid breedId)
    {
        var speciesAndBreed = new SpeciesAndBreed(speciesId, breedId);

        return speciesAndBreed;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return SpeciesId;
        yield return BreedId;
    }
}
