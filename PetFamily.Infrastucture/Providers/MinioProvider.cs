using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using System.IO;

namespace PetFamily.Infrastucture.Providers
{
    public class MinioProvider : IFileProvider
    {
        private const int EXPIRY = 60 * 60 * 24;
        private const int MAX_DEGREE_OF_PARALLELIZM = 10;

        private readonly IMinioClient _minioClient;
        private readonly ILogger<MinioProvider> _logger;

        public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger) 
        {
            _minioClient = minioClient;
            _logger = logger;
        }   

        public async Task<UnitResult<Error>> Uploadfiles(
            FileData fileData,
            CancellationToken cancellationToken = default) 
        {
            var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELIZM);

            try
            {
                var bucketExistArgs = new BucketExistsArgs().WithBucket(fileData.BucketName);

                var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
                if (bucketExist == false)
                {
                    var makeBucketArgs = new MakeBucketArgs().WithBucket(fileData.BucketName);

                    await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
                }

                var tasks = new List<Task>();

                foreach (var file in fileData.Files)
                {
                    await semaphoreSlim.WaitAsync(cancellationToken);

                    var putObjectArgs = new PutObjectArgs()
                        .WithBucket(fileData.BucketName)
                        .WithStreamData(file.Stream)
                        .WithObjectSize(file.Stream.Length)
                        .WithObject(file.ObjectName);

                    var task = _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

                    semaphoreSlim.Release();
                    tasks.Add(task);
                }

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to upload file in minio");
                return Error.Failure("file.upload", "Fail to upload file in minio");
            }
            finally
            {
                semaphoreSlim.Release();
            }

            return Result.Success<Error>();
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
    }
}
