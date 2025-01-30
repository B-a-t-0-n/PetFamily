using Microsoft.Extensions.Logging;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Messaging;
using PetFamily.Application.Providers;


namespace PetFamily.Infrastucture.Files
{
    public class FilesCleanerService : IFilesCleanerService
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<FilesCleanerService> _logger;
        private readonly IMessageQueue<IEnumerable<FileMetadata>> _messageQueue;

        public FilesCleanerService(
            IFileProvider fileProvider,
            ILogger<FilesCleanerService> logger,
            IMessageQueue<IEnumerable<FileMetadata>> messageQueue)
        {
            _fileProvider = fileProvider;
            _logger = logger;
            _messageQueue = messageQueue;
        }

        public async Task Process(CancellationToken stoppingToken)
        {
            var fileInfos = await _messageQueue.ReadAsync(stoppingToken);

            foreach (var fileInfo in fileInfos)
            {
                await _fileProvider.Deletefile(fileInfo, stoppingToken);
            }
        }
    }
}
