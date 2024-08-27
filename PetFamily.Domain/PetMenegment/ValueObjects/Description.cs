using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class Description : ValueObject
    {
        public const int MAX_HIGHT_DESCRIPTION_LENGTH = 6000;

        private Description() { }
        private Description(string value)
        {
            Value = value;
        }

        public string? Value { get; } = default!;

        public static Result<Description> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                Result.Failure<Description>("description is null or white space");

            if (value.Length > MAX_HIGHT_DESCRIPTION_LENGTH)
                Result.Failure<Description>($"description > {MAX_HIGHT_DESCRIPTION_LENGTH}");

            var description = new Description(value);

            return description;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value == null ? "" : Value;
        }
    }
}
