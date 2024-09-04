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

        public static Result<Nickname, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Errors.General.ValueIsInvalid("nickname");

            if (value.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsRequired("nickname");

            var name = new Nickname(value);

            return name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
