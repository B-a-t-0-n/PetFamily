using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.Shared.IDs
{
    public class PetPhotoId : ValueObject
    {
        private PetPhotoId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static PetPhotoId NewPetPhotoId() => new PetPhotoId(Guid.NewGuid());

        public static PetPhotoId Empty() => new PetPhotoId(Guid.Empty);

        public static PetPhotoId Create(Guid id) => new(id);

        public static implicit operator Guid(PetPhotoId id) => id.Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
