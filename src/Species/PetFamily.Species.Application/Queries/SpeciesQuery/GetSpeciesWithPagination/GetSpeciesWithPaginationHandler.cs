using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;
using PetFamily.Core.Extentions;
using PetFamily.Core.Models;

namespace PetFamily.Species.Application.Queries.SpeciesQuery.GetSpeciesWithPagination;

public class GetSpeciesWithPaginationHandler : IQueryHandler<PagedList<SpeciesDto>, GetSpeciesWithPaginationQuery>
{
    private readonly IReadSpeciesDbContext _readDbContext;
    private readonly ILogger<GetSpeciesWithPaginationHandler> _logger;


    public GetSpeciesWithPaginationHandler(
        IReadSpeciesDbContext readDbContext,
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
