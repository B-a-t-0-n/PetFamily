using Microsoft.EntityFrameworkCore;
using PetFamily.Infrastucture.DbContexts;

namespace PetFamily.Infrastucture.Service
{
    public class DeleteExpiredEntityService
    {
        private readonly WriteDbContext _writeDbContext;

        public DeleteExpiredEntityService(WriteDbContext writeDbContext)
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

                if(volunteer.IsExpired())
                {
                    _writeDbContext.Volunteers.Remove(volunteer);
                }
            }

            await _writeDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
