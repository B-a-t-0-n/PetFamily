namespace PetFamily.Application.FileProviders
{
    public record FileData(Stream Stream, string BucketName, string ObjectName);
}
