using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System.Text.RegularExpressions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public const int MAX_HIGHT_PHONE_NUMBER_LENGTH = 20;

        private const string PHONE_REGEX = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";

        private PhoneNumber() { }
        private PhoneNumber(string number)
        {
            Number = number;
        }

        public string Number { get; } = default!;

        public static Result<PhoneNumber, Error> Create(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return Errors.General.ValueIsInvalid("number");

            if (number.Length > MAX_HIGHT_PHONE_NUMBER_LENGTH)
                return Errors.General.ValueIsRequired("number");

            if (Regex.IsMatch(number, PHONE_REGEX) == false)
                return Errors.General.ValueIsInvalid("number");

            var phoneNumber = new PhoneNumber(number);

            return phoneNumber;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
