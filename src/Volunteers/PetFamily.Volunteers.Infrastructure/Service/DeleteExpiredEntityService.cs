using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Volunteers.Infrastructure.DbContexts;

namespace PetFamily.Volunteers.Infrastructure.Service;

public class DeleteExpiredEntityService
{
    private readonly WriteVolunteersDbContext _writeDbContext;
    private readonly ILogger<DeleteExpiredEntityService> _logger;

    public DeleteExpiredEntityService(
        WriteVolunteersDbContext writeDbContext,
        ILogger<DeleteExpiredEntityService> logger)
    {
        _writeDbContext = writeDbContext;
        _logger = logger;
    }

    public async Task Process(CancellationToken cancellationToken)
    {
        try
        {
            var volunteers = await _writeDbContext.Volunteers
            .Include(v => v.Pets)
            .ToListAsync(cancellationToken);

            foreach (var volunteer in volunteers)
            {
                volunteer.DeleteExpiredPets();

                if (volunteer.IsExpired())
                {
                    _writeDbContext.Volunteers.Remove(volunteer);
                }
            }

            await _writeDbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            _logger.LogError("Error occurred while deleting expired entities.");
        }
    }
}
