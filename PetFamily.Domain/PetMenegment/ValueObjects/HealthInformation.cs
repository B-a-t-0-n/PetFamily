using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class HealthInformation : ValueObject
    {
        private HealthInformation() { }
        private HealthInformation(string value)
        {
            Value = value;
        }

        public string? Value { get; } = default!;

        public static Result<HealthInformation, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Errors.General.ValueIsInvalid("healthInformation");

            if (value.Length > Constants.MAX_HIGHT_TEXT_LENGTH)
                return Errors.General.ValueIsRequired("healthInformation");

            var healthInformation = new HealthInformation(value);

            return healthInformation;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value == null ? "" : Value;
        }
    }
}

