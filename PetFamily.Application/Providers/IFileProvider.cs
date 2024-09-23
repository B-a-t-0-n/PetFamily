using CSharpFunctionalExtensions;
using PetFamily.Application.FileProviders;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Providers
{
    public interface IFileProvider
    {
        Task<Result<string, Error>> Uploadfile(FileData fileData, CancellationToken cancellationToken = default);
        Task<Result<string, Error>> Deletefile(string bucketName, string objectName, CancellationToken cancellationToken = default);
        Task<Result<string, Error>> GetfileURL(string bucketName, string objectName, CancellationToken cancellationToken = default);
    }
}
