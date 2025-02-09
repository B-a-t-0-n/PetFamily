using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.SpeciesMenegment.ValueObjects
{
    public class Name : ValueObject
    {
        private Name() { }
        private Name(string value)
        {
            Value = value;
        }

        public string Value { get; } = default!;

        public static Result<Name> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<Name>("Name is null or white space");

            if (value.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Result.Failure<Name>($"Name > {Constants.MAX_LOW_TEXT_LENGTH}");

            var name = new Name(value);

            return name;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}