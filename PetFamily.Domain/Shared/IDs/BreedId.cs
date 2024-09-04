namespace PetFamily.Domain.Shared.IDs
{
    public class BreedId : ValueObject
    {
        private BreedId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static BreedId NewBreedId() => new BreedId(Guid.NewGuid());

        public static BreedId Empty() => new BreedId(Guid.Empty);

        public static BreedId Create(Guid id) => new(id);

        public static implicit operator Guid(BreedId id) => id.Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
