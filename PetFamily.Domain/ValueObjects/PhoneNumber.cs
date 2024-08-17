using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private const string PHONE_REGEX = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";

        private PhoneNumber(string number)
        {
            Number = number;
        }

        public string Number { get; }

        public static Result<PhoneNumber> Create(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                Result.Failure<PhoneNumber>("number is null or white space");

            if (Regex.IsMatch(number, PHONE_REGEX) == false)
                Result.Failure<PhoneNumber>("number does not match PHONE_REGEX");

            var phoneNumber = new PhoneNumber(number);

            return phoneNumber;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
