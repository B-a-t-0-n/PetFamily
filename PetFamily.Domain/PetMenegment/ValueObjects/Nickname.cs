using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class Nickname : ValueObject
    {
        private Nickname() { }
        private Nickname(string value)
        {
            Value = value;   
        }

        public string Value { get; } = default!;

        public static Result<Nickname> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<Nickname>("Nickname is null or white space");

            if (value.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Result.Failure<Nickname>($"Nickname > {Constants.MAX_LOW_TEXT_LENGTH}");

            var name = new Nickname(value);

            return name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
