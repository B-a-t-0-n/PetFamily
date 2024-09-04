namespace PetFamily.Domain.Shared.IDs
{
    public class SpeciesId : ValueObject
    {
        private SpeciesId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static SpeciesId NewSpeciesId() => new SpeciesId(Guid.NewGuid());

        public static SpeciesId Empty() => new SpeciesId(Guid.Empty);

        public static SpeciesId Create(Guid id) => new(id);

        public static implicit operator Guid(SpeciesId id) => id.Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
