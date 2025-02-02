using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.PetManagement;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Infrastucture.DbContexts;

namespace PetFamily.Infrastucture.Repositories
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly WriteDbContext _dbContext;

        public VolunteerRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
            return volunteer.Id;
        }

        public async Task<Result<Volunteer, Error>> GetById(VolunteerId id, CancellationToken cancellationToken = default)
        {
            var volunteer = await _dbContext.Volunteers
                .Include(v => v.Pets)
                .ThenInclude(p => p.PetPhotos)
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

            if (volunteer is null)
                return Errors.General.NotFound(id);

            return volunteer;
        }

        public Guid Save(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            _dbContext.Volunteers.Attach(volunteer);

            return volunteer.Id;
        }

        public Guid Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            _dbContext.Volunteers.Remove(volunteer);

            return volunteer.Id;
        }
    }
}
