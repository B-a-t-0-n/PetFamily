using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Species.Application;
using PetFamily.Species.Infrastructure.DbContexts;

namespace PetFamily.Species.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteSpeciesDbContext _dbContext;

    public SpeciesRepository(WriteSpeciesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Domain.Entity.Species species, CancellationToken cancellationToken = default)
    {
        await _dbContext.Species.AddAsync(species, cancellationToken);

        return species.Id;
    }

    public Guid Delete(Domain.Entity.Species species, CancellationToken cancellationToken = default)
    {
        _dbContext.Species.Remove(species);

        return species.Id;
    }

    public async Task<Result<Domain.Entity.Species, Error>> GetById(SpeciesId id, CancellationToken cancellationToken = default)
    {
        var species = await _dbContext.Species.Include(s => s.breeds).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (species is null)
            return Errors.General.NotFound(id);

        return species;
    }

    public Guid Save(Domain.Entity.Species species, CancellationToken cancellationToken = default)
    {
        _dbContext.Species.Attach(species);

        return species.Id;
    }
}
