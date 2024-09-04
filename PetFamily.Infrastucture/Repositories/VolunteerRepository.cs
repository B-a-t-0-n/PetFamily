using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Infrastucture.Repositories
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VolunteerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return volunteer.Id;
        }

        public async Task<Result<Volunteer>> GetById(VolunteerId id)
        {
            var volunteer = await _dbContext.Volunteers
                .Include(v => v.Pets)
                .ThenInclude(p => p.PetPhotos)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (volunteer is null)
                return Result.Failure<Volunteer>("volunteer is null");

            return Result.Success(volunteer);
        }
    }
}
