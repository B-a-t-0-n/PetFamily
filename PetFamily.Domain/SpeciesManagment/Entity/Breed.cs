using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.SpeciesMenegment.ValueObjects;

namespace PetFamily.Domain.SpeciesMenegment.Entity
{
    public class Breed : Shared.Entity<BreedId>
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
}
