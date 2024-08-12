using CSharpFunctionalExtensions;

namespace PetFamily.Domain
{
    public class PetPhoto
    {
        //ef core
        private PetPhoto() 
        {

        }

        private PetPhoto(string path, bool isMain)
        {
            Path = path;
            IsMain = isMain;
        }

        public Guid Id { get; protected set; }
        public string Path { get; protected set; } = default!;
        public bool IsMain { get; protected set; }

        public static Result<PetPhoto> Create(string path, bool isMain)
        {
            if (string.IsNullOrWhiteSpace(path))
                Result.Failure<PetPhoto>("path is null or white space");

            var pet = new PetPhoto(path, isMain);

            return Result.Success(pet);
        }
    }
}
