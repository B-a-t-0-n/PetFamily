using CSharpFunctionalExtensions;
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

        public static Result<PhoneNumber> Create(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                Result.Failure<PhoneNumber>("number is null or white space");

            if (number.Length > MAX_HIGHT_PHONE_NUMBER_LENGTH)
                Result.Failure<PhoneNumber>($"number > {MAX_HIGHT_PHONE_NUMBER_LENGTH}");

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
