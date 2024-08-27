using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class Address : ValueObject
    {
        private Address() { }
        private Address(string city, string street, string house, string? flat, string? apartmentNumber)
        {
            Сity = city;
            Street = street;
            House = house;
            Flat = flat;
            ApartmentNumber = apartmentNumber;
        }

        public string Сity { get; } = default!;
        public string Street { get; } = default!;
        public string House { get; } = default!;
        public string? Flat { get; }
        public string? ApartmentNumber { get; }

        public static Result<Address> Create(string city, string street, string house, string? flat, string? apartmentNumber)
        {
            if (string.IsNullOrWhiteSpace(city))
                Result.Failure<Address>("city is null or white space");

            if (city.Length > Constants.MAX_LOW_TEXT_LENGTH)
                Result.Failure<Address>($"city > {Constants.MAX_LOW_TEXT_LENGTH}");

            if (string.IsNullOrWhiteSpace(street))
                Result.Failure<Address>("street is null or white space");

            if (street.Length > Constants.MAX_LOW_TEXT_LENGTH)
                Result.Failure<Address>($"street > {Constants.MAX_LOW_TEXT_LENGTH}");

            if (string.IsNullOrWhiteSpace(house))
                Result.Failure<Address>("house is null or white space");

            if (house.Length > Constants.MAX_LOW_TEXT_LENGTH)
                Result.Failure<Address>($"house > {Constants.MAX_LOW_TEXT_LENGTH}");

            if (flat != null && flat!.Length > Constants.MAX_LOW_TEXT_LENGTH)
                Result.Failure<Address>($"flat > {Constants.MAX_LOW_TEXT_LENGTH}");

            if (apartmentNumber != null && apartmentNumber!.Length > Constants.MAX_LOW_TEXT_LENGTH)
                Result.Failure<Address>($"apartmentNumber > {Constants.MAX_LOW_TEXT_LENGTH}");

            var address = new Address(city, street, house, flat, apartmentNumber);

            return address;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Сity;
            yield return Street;
            yield return House;
            yield return Flat == null ? "" : Flat;
            yield return ApartmentNumber == null ? "" : ApartmentNumber!;
        }

    }
}
