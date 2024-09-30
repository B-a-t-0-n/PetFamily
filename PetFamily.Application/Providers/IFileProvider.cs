using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Providers
{
    public interface IFileProvider
    {
        Task<UnitResult<Error>> Uploadfiles(FileData filesData, CancellationToken cancellationToken = default);
        Task<Result<string, Error>> Deletefile(FileMetadata fileMetadata, CancellationToken cancellationToken = default);
        Task<Result<string, Error>> GetfileURL(FileMetadata fileMetadata, CancellationToken cancellationToken = default);
    }
}
