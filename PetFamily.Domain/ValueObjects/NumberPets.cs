using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.ValueObjects
{
    public class NumberPets : ValueObject
    {
        private NumberPets(int foundAHouse, int lookingForHouse, int beingTreated)
        {
            FoundAHouse = foundAHouse;
            LookingForHouse = lookingForHouse;
            BeingTreated = beingTreated;
        }

        public int FoundAHouse { get; }
        public int LookingForHouse { get; }
        public int BeingTreated { get; }

        public static Result<NumberPets> Create(int foundAHouse, int lookingForHouse, int beingTreated)
        {
            if (foundAHouse < 0)
                Result.Failure<NumberPets>("foundAHouse < 0");

            if (lookingForHouse < 0)
                Result.Failure<NumberPets>("lookingForHouse < 0");

            if (beingTreated < 0)
                Result.Failure<NumberPets>("beingTreated < 0");

            var numberPets = new NumberPets(foundAHouse, lookingForHouse, beingTreated);

            return numberPets;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FoundAHouse;
            yield return LookingForHouse;
            yield return BeingTreated;
        }
    }
}
