using CSharpFunctionalExtensions;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Domain.PetMenegment.Entity
{
    public class PetPhoto : Shared.Entity<PetPhotoId>
    {
        //ef core
        private PetPhoto(PetPhotoId id) : base(id) { }

        private PetPhoto(PetPhotoId id, PhotoPath path, bool isMain) : base(id)
        {
            Path = path;
            IsMain = isMain;
        }

        public PhotoPath Path { get; private set; } = default!;
        public bool IsMain { get; private set; }

        public static Result<PetPhoto, Error> Create(PetPhotoId id, PhotoPath path, bool isMain)
        {
            var pet = new PetPhoto(id , path, isMain);

            return pet;
        }
    }
}
