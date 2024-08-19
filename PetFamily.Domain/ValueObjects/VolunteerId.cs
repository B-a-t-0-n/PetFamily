using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.ValueObjects
{
    public class VolunteerId : ValueObject
    {
        private VolunteerId() { }
        private VolunteerId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static VolunteerId NewPetId() => new VolunteerId(Guid.NewGuid());

        public static VolunteerId Empty() => new VolunteerId(Guid.Empty);

        public static VolunteerId Create(Guid id) => new(id);
         
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
