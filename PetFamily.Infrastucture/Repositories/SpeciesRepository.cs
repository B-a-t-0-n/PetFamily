using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.SpeciesManagment;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.SpeciesMenegment.Entity;
using PetFamily.Infrastucture.DbContexts;

namespace PetFamily.Infrastucture.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly WriteDbContext _dbContext;

        public SpeciesRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(Species species, CancellationToken cancellationToken = default)
        {
            await _dbContext.Species.AddAsync(species, cancellationToken);

            return species.Id;
        }

        public Guid Delete(Species species, CancellationToken cancellationToken = default)
        {
            _dbContext.Species.Remove(species);

            return species.Id;
        }

        public async Task<Result<Species, Error>> GetById(SpeciesId id, CancellationToken cancellationToken = default)
        {
            var species = await _dbContext.Species.Include(s => s.breeds).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (species is null)
                return Errors.General.NotFound(id);

            return species;
        }

        public Guid Save(Species species, CancellationToken cancellationToken = default)
        {
            _dbContext.Attach(species);

            return species.Id;
        }
    }
}
