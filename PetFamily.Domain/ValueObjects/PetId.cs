using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.ValueObjects
{
    public class PetId : ValueObject
    {
        private PetId() { }
        private PetId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static PetId NewPetId() => new PetId(Guid.NewGuid());

        public static PetId Empty() => new PetId(Guid.Empty); 

        public static PetId Create(Guid id) => new(id);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
