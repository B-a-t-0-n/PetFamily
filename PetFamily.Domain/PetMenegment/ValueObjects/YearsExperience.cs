using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

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

        public static Result<YearsExperience> Create(int value)
        {
            if (value < 0)
                Result.Failure<YearsExperience>($"years experience < 0");

            var yearsExperience = new YearsExperience(value);

            return yearsExperience;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
