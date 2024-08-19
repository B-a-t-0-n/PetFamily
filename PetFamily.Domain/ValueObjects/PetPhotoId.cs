using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.ValueObjects
{
    public class PetPhotoId : ValueObject
    {
        private PetPhotoId() { }
        private PetPhotoId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static PetPhotoId NewPetId() => new PetPhotoId(Guid.NewGuid());

        public static PetPhotoId Empty() => new PetPhotoId(Guid.Empty);

        public static PetPhotoId Create(Guid id) => new(id);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
