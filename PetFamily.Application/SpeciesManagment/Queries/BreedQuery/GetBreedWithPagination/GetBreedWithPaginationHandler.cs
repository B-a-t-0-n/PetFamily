using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extentions;
using PetFamily.Application.Models;

namespace PetFamily.Application.SpeciesManagment.Queries.BreedQuery.GetBreedWithPagination
{
    public class GetBreedWithPaginationHandler : IQueryHandler<PagedList<BreedDto>, GetBreedWithPaginationQuery>
    {
        private readonly IReadDbContext _readDbContext;
        private readonly ILogger<GetBreedWithPaginationHandler> _logger;


        public GetBreedWithPaginationHandler(
            IReadDbContext readDbContext,
            ILogger<GetBreedWithPaginationHandler> logger)
        {
            _readDbContext = readDbContext;
            _logger = logger;
        }

        public async Task<PagedList<BreedDto>> Handle(
            GetBreedWithPaginationQuery query,
            CancellationToken cancellationToken = default)
        {
            var breedsQuery = _readDbContext.Breeds;

            breedsQuery = breedsQuery.Where(b => b.SpeciesId == query.SpeciesId);

            var pagedList = await breedsQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

            _logger.LogInformation("received breeds with page {page} of page size {PageSize} in species with id {speciesId}",
                query.Page,
                query.PageSize,
                query.SpeciesId);

            return pagedList;
        }
    }
}
