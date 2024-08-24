using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class Color : ValueObject
    {
        private Color() { }
        private Color(string? value)
        {
            Value = value;
        }

        public string? Value { get; } = default!;

        public static Result<Color> Create(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                Result.Failure<Color>("Color is null or white space");

            if (value != null && value.Length > Constants.MAX_LOW_TEXT_LENGTH)
                Result.Failure<Color>($"Color > {Constants.MAX_LOW_TEXT_LENGTH}");

            var color = new Color(value);

            return color;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value == null ? "" : Value;
        }
    }
}
