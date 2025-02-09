using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extentions;
using PetFamily.Application.Models;

namespace PetFamily.Application.SpeciesManagment.Queries.SpeciesQuery.GetSpeciesWithPagination
{
    public class GetSpeciesWithPaginationHandler : IQueryHandler<PagedList<SpeciesDto>, GetSpeciesWithPaginationQuery>
    {
        private readonly IReadDbContext _readDbContext;
        private readonly ILogger<GetSpeciesWithPaginationHandler> _logger;


        public GetSpeciesWithPaginationHandler(
            IReadDbContext readDbContext,
            ILogger<GetSpeciesWithPaginationHandler> logger)
        {
            _readDbContext = readDbContext;
            _logger = logger;
        }

        public async Task<PagedList<SpeciesDto>> Handle(
            GetSpeciesWithPaginationQuery query,
            CancellationToken cancellationToken = default)
        {
            var speciesQuery = _readDbContext.Species;

            var pagedList = await speciesQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

            _logger.LogInformation("received species with page {page} of page size {PageSize}",
                query.Page,
                query.PageSize);

            return pagedList;
        }
    }
}
