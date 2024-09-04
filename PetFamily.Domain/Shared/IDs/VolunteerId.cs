using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.Shared.IDs
{
    public class VolunteerId : ValueObject
    {
        private VolunteerId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static VolunteerId NewVolunteerId() => new VolunteerId(Guid.NewGuid());

        public static VolunteerId Empty() => new VolunteerId(Guid.Empty);

        public static VolunteerId Create(Guid id) => new(id);

        public static implicit operator Guid(VolunteerId id) => id.Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
