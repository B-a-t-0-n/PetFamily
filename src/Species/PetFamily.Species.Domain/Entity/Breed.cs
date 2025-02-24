using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Species.Domain.ValueObjects;

namespace PetFamily.Species.Domain.Entity;

public class Breed : SharedKernel.Entity<BreedId>
{
    //ef core
    private Breed(BreedId id) : base(id) { }

    private Breed(BreedId id, Name name) : base(id)
    {
        Name = name;
    }

    public Name Name { get; private set; } = default!;

    public static Result<Breed, Error> Create(BreedId id, Name name)
    {
        var breed = new Breed(id, name);

        return breed;
    }
}
