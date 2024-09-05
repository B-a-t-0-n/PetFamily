using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using ValueObject = PetFamily.Domain.Shared.ValueObject;
using Error = PetFamily.Domain.Shared.Error;

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

        public static Result<Address, Error> Create(string city, string street, string house, string? flat, string? apartmentNumber)
        {
            if (string.IsNullOrWhiteSpace(city))
                return Errors.General.ValueIsInvalid("city");

            if (city.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsRequired("city");

            if (string.IsNullOrWhiteSpace(street))
                return Errors.General.ValueIsInvalid("street");

            if (street.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsRequired("street");

            if (string.IsNullOrWhiteSpace(house))
                return Errors.General.ValueIsInvalid("house");

            if (house.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsRequired("house");

            if (flat != null && flat!.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsRequired("flat");

            if (apartmentNumber != null && apartmentNumber!.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsRequired("apartmentNumber");

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
