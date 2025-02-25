using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Species.Domain.ValueObjects;

namespace PetFamily.Species.Domain.Entity;

public class Species : SharedKernel.Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];

    //ef core
    private Species(SpeciesId id) : base(id) { }

    private Species(SpeciesId id, Name name) : base(id)
    {
        Name = name;
    }

    public Name Name { get; private set; } = default!;

    public IReadOnlyList<Breed> breeds => _breeds;

    public static Result<Species, Error> Create(SpeciesId id, Name name)
    {
        var species = new Species(id, name);

        return species;
    }

    public UnitResult<Error> AddBreed(Breed breed)
    {
        if (_breeds.Any(b => b.Name.Value.Equals(breed.Name.Value, StringComparison.CurrentCultureIgnoreCase)))
            return UnitResult.Failure(Errors.General.AlreadyExist());

        _breeds.Add(breed);
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> RemoveBreed(Breed breed)
    {
        if (_breeds.Any(b => b.Id == breed.Id) == false)
            return UnitResult.Failure(Errors.General.NotFound());

        _breeds.Remove(breed);
        return UnitResult.Success<Error>();
    }
}
