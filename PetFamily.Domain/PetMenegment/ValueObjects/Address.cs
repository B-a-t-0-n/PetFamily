using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class Address : ValueObject
    {
        private Address() { }
        private Address(string city, string street, string house, string flat)
        {
            Сity = city;
            Street = street;
            House = house;
            Flat = flat;
        }

        public string Сity { get; } = default!;
        public string Street { get; } = default!;
        public string House { get; } = default!;
        public string Flat { get; } = default!;

        public static Result<Address> Create(string city, string street, string house, string flat)
        {
            if (string.IsNullOrWhiteSpace(city))
                Result.Failure<Address>("city is null or white space");

            if (string.IsNullOrWhiteSpace(street))
                Result.Failure<Address>("street is null or white space");

            if (string.IsNullOrWhiteSpace(house))
                Result.Failure<Address>("house is null or white space");

            if (string.IsNullOrWhiteSpace(flat))
                Result.Failure<Address>("flat is null or white space");

            var detailsForAssistance = new Address(city, street, house, flat);

            return detailsForAssistance;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Сity;
            yield return Street;
            yield return House;
            yield return Flat;
        }

    }
}
