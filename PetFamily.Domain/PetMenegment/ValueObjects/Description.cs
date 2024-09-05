using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class Description : ValueObject
    {
        public const int MAX_HIGHT_DESCRIPTION_LENGTH = 6000;

        private Description() { }
        private Description(string? value)
        {
            Value = value;
        }

        public string? Value { get; } = default!;

        public static Result<Description, Error> Create(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Errors.General.ValueIsInvalid("description");

            if (value.Length > MAX_HIGHT_DESCRIPTION_LENGTH)
                return Errors.General.ValueIsRequired("description");

            var description = new Description(value);

            return description;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value == null ? "" : Value;
        }
    }
}
