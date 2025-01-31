using PetFamily.Domain.PetMenegment.ValueObjects;

namespace PetFamily.Application.FileProvider
{
    public record FileData(Stream Stream, PhotoPath FilePath, string BucketName);
}
