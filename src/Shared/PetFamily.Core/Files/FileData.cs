using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Core.Files;

public record FileData(Stream Stream, PhotoPath FilePath, string BucketName);
