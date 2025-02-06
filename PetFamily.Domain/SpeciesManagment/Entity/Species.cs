using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.SpeciesMenegment.ValueObjects;

namespace PetFamily.Domain.SpeciesMenegment.Entity
{
    public class Species : Shared.Entity<SpeciesId>
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

        public static Result<Species> Create(SpeciesId id, Name name)
        {
            var pet = new Species(id, name);

            return Result.Success(pet);
        }
    }

}
