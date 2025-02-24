using Microsoft.EntityFrameworkCore;
using PetFamily.Volunteers.Infrastructure.DbContexts;

namespace PetFamily.Volunteers.Infrastructure.Service;

public class DeleteExpiredEntityService
{
    private readonly WriteVolunteersDbContext _writeDbContext;

    public DeleteExpiredEntityService(WriteVolunteersDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task Process(CancellationToken cancellationToken)
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
}
