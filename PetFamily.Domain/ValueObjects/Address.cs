using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        private Address(string city, string street, string house, string flat)
        {
            Сity = city;
            Street = street;
            House = house;
            Flat = flat;
        }

        public string Сity { get; protected set; } = default!;
        public string Street { get; protected set; } = default!;
        public string House { get; protected set; } = default!;
        public string Flat { get; protected set; } = default!;

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
