using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class SerialNumber : ValueObject
    {
        private SerialNumber() { }  
        private SerialNumber(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public Result<SerialNumber, Error> Forward()
        {
            return Create(Value + 1);
        }

        public Result<SerialNumber, Error> Back()
        {
            return Create(Value - 1);
        }

        public static Result<SerialNumber, Error> Create(int number)
        {
            if (number <= 0)
                return Errors.General.ValueIsInvalid("serial number");

            return new SerialNumber(number);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
