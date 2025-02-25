using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;
using PetFamily.Core.Extentions;
using PetFamily.Core.Models;

namespace PetFamily.Volunteers.Application.Queries.VolunteerHandlers.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
{
    private readonly IReadVolunteersDbContext _readDbContext;
    private readonly ILogger<GetVolunteersWithPaginationHandler> _logger;


    public GetVolunteersWithPaginationHandler(
        IReadVolunteersDbContext readDbContext,
        ILogger<GetVolunteersWithPaginationHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<PagedList<VolunteerDto>> Handle(
        GetVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        var pagedList = await volunteersQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

        _logger.LogInformation("received volunteers with page {page} of page size {PageSize}",
            query.Page,
            query.PageSize);

        return pagedList;
    }
}
