using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects
{
    public class PhysicalQuantity : ValueObject
    {
        public static readonly PhysicalQuantity Km = new(nameof(Km));
        public static readonly PhysicalQuantity M = new(nameof(M));
        public static readonly PhysicalQuantity Cm = new(nameof(Cm));
        public static readonly PhysicalQuantity Mm = new(nameof(Mm));
        public static readonly PhysicalQuantity T = new(nameof(T));
        public static readonly PhysicalQuantity Kg = new(nameof(Kg));
        public static readonly PhysicalQuantity G = new(nameof(G));

        private static readonly PhysicalQuantity[] _all = [Km, M, Cm, Mm, T, Kg, G];

        private PhysicalQuantity(string type, double value = 0)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; protected set; }
        public double Value { get; protected set; }

        public static Result<PhysicalQuantity> Create(string type, double value)
        {
            if (string.IsNullOrWhiteSpace(type))
                Result.Failure("type is null or white space");

            var typeInput = type.Trim().ToLower();

            if (_all.Any(p => p.Type.ToLower() == typeInput) == false)
                Result.Failure("error type");

            if (value < 0)
                Result.Failure("value < 0");

            var physicalQuantity = new PhysicalQuantity(typeInput, value);

            return physicalQuantity;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Value;
        }
    }
}
