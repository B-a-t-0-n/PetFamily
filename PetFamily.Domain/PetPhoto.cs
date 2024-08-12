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

        public Guid Id { get; private set; }
        public string Path { get; private set; } = default!;
        public bool IsMain { get; private set; }

        public static Result<PetPhoto> Create(string path, bool isMain)
        {
            if (string.IsNullOrWhiteSpace(path))
                Result.Failure<PetPhoto>("path is null or white space");

            var pet = new PetPhoto(path, isMain);

            return Result.Success(pet);
        }
    }
}
