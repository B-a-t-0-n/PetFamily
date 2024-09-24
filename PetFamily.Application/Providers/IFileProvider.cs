using CSharpFunctionalExtensions;
using PetFamily.Application.FileProviders;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Providers
{
    public interface IFileProvider
    {
        Task<Result<string, Error>> Uploadfile(FileData fileData, CancellationToken cancellationToken = default);
        Task<Result<string, Error>> Deletefile(FileMetadata fileMetadata, CancellationToken cancellationToken = default);
        Task<Result<string, Error>> GetfileURL(FileMetadata fileMetadata, CancellationToken cancellationToken = default);
    }
}
