using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class YearsExperience : ValueObject
    {
        private YearsExperience() { }
        private YearsExperience(int value)
        {
            Value = value;
        }

        public int Value { get; } = default!;

        public static Result<YearsExperience, Error> Create(int value)
        {
            if (value < 0)
                return Errors.General.ValueIsRequired("yearsExperience");

            var yearsExperience = new YearsExperience(value);

            return yearsExperience;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
