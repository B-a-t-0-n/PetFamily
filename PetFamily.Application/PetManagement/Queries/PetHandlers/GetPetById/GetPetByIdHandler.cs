using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.PetManagement.Queries.PetHandlers.GetPetById
{
    public class GetPetByIdHandler : IQueryHandler<PetDto?, GetPetByIdQuery>
    {
        private readonly IReadDbContext _readDbContext;
        private readonly ILogger<GetPetByIdHandler> _logger;


        public GetPetByIdHandler(
            IReadDbContext readDbContext,
            ILogger<GetPetByIdHandler> logger)
        {
            _readDbContext = readDbContext;
            _logger = logger;
        }

        public async Task<PetDto?> Handle(
            GetPetByIdQuery query,
            CancellationToken cancellationToken = default)
        {
            var pet = await _readDbContext.Pets.FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

            _logger.LogInformation("received pet with id {id}", query.Id);

            return pet;
        }
    }
}

