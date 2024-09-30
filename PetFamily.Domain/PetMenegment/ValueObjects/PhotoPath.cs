using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class PhotoPath : ValueObject
    {
        public static readonly string[] extensions = [".png", ".jpg", ".bmp", ".jpeg", ".gif"];

        private PhotoPath() { }
        private PhotoPath(string path)
        {
            PathToStorage = path;
        }

        public string PathToStorage { get; } = default!;

        public static Result<PhotoPath, Error> Create(string name, string extesion)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValueIsInvalid("name");

            if (string.IsNullOrWhiteSpace(extesion))
                return Errors.General.ValueIsInvalid("extesion");

            if (extensions.Any(s => s == extesion) == false)
                return Errors.General.ValueIsInvalid("extesion");

            var path = $"{name}{extesion}";

            var photoPath = new PhotoPath(path);

            return photoPath;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PathToStorage;
        }
    }
}
