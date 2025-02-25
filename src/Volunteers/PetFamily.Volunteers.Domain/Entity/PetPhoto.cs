using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;

namespace PetFamily.Volunteers.Domain.Entity;

public class PetPhoto : SharedKernel.Entity<PetPhotoId>
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
        var petPhoto = new PetPhoto(id , path, isMain);

        return petPhoto;
    }

    internal void SetIsMain(bool status)
    {
        IsMain = status;
    }
}
