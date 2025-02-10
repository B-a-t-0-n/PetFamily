using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetFamily.Infrastucture.Service;

namespace PetFamily.Infrastucture.BackgroundServices
{
    public class DeleteExpiredEntityBackgroundService : BackgroundService
    {
        private readonly ILogger<DeleteExpiredEntityBackgroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public DeleteExpiredEntityBackgroundService(
            ILogger<DeleteExpiredEntityBackgroundService> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("DeleteExpiredEntityBackgroundService is running.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await using var scope = _scopeFactory.CreateAsyncScope();

                var deleteExpiredEntityService = scope.ServiceProvider
                    .GetRequiredService<DeleteExpiredEntityService>();

                _logger.LogInformation("DeleteExpiredEntityBackgroundService is working.");

                await deleteExpiredEntityService.Process(stoppingToken);

                await Task.Delay(TimeSpan.FromHours(Constants.DELETE_EXPECTED_PET_SERVICE_REDUCTION_HOURS), stoppingToken);
            }
        }
    }
}
