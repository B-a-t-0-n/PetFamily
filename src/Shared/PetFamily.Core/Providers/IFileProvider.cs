using CSharpFunctionalExtensions;
using PetFamily.Core.Files;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Core.Providers;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<PhotoPath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken = default);
    Task<Result<string, Error>> Deletefile(FileMetadata fileMetadata, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetfileURL(FileMetadata fileMetadata, CancellationToken cancellationToken = default);
}
