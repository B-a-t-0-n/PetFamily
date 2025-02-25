using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Core.Files;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private const int EXPIRY = 60 * 60 * 24;
    private const int MAX_DEGREE_OF_PARALLELISM = 10;

    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<PhotoPath>, Error>> UploadFiles(IEnumerable<FileData> filesData, CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);
        var filesList = filesData.ToList();

        try
        {
            await IfBucketsNotExistCreateBucket(filesList, cancellationToken);

            var tasks = filesList.Select(async file =>
                await PutObject(file, semaphoreSlim, cancellationToken));

            var pathsResult = await Task.WhenAll(tasks);

            if (pathsResult.Any(p => p.IsFailure))
                return pathsResult.First().Error;

            var results = pathsResult.Select(p => p.Value).ToList();

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload files in minio, files amount: {amount}", filesList.Count);
            return Error.Failure("file.upload", "Fail to upload files in minio");
        }
    }

    public async Task<Result<string, Error>> Deletefile(
        FileMetadata fileMetadata,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucetExistArgs = new BucketExistsArgs().WithBucket(fileMetadata.BucketName);

            var bucketExist = await _minioClient.BucketExistsAsync(bucetExistArgs, cancellationToken);
            if (bucketExist == false)
                throw new Exception($"Bucket {fileMetadata.BucketName} not exist");

            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(fileMetadata.BucketName)
                .WithObject(fileMetadata.ObjectName);

            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);

            return "File is deleted";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to delete file in minio");
            return Error.Failure("file.delete", "Fail to delete file in minio");
        }

    }

    public async Task<Result<string, Error>> GetfileURL(
        FileMetadata fileMetadata,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucetExistArgs = new BucketExistsArgs().WithBucket(fileMetadata.BucketName);

            var bucketExist = await _minioClient.BucketExistsAsync(bucetExistArgs, cancellationToken);
            if (bucketExist == false)
                throw new Exception($"Bucket {fileMetadata.BucketName} not exist");

            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(fileMetadata.BucketName)
                .WithObject(fileMetadata.ObjectName)
                .WithExpiry(EXPIRY);

            var url = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);

            return url;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to get file in minio");
            return Error.Failure("file.get", "Fail to get file in minio");
        }

    }

    private async Task<Result<PhotoPath, Error>> PutObject(
        FileData fileData,
        SemaphoreSlim semaphoreSlim,
    CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileData.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.FilePath.PathToStorage);

        try
        {
            await _minioClient
                .PutObjectAsync(putObjectArgs, cancellationToken);
            return fileData.FilePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.FilePath.PathToStorage,
                fileData.BucketName);
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task IfBucketsNotExistCreateBucket(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = [.. filesData.Select(file => file.BucketName)];

        foreach (var bucketName in bucketNames)
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);
            var bucketExist = await _minioClient
                .BucketExistsAsync(bucketExistArgs, cancellationToken);
            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }


}
