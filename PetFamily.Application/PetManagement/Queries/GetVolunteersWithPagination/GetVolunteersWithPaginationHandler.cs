using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extentions;
using PetFamily.Application.Models;


namespace PetFamily.Application.PetManagement.Queries.GetVolunteersWithPagination
{
    public class GetVolunteersWithPaginationHandler
    {
        private readonly IReadDbContext _readDbContext;
        private readonly ILogger<GetVolunteersWithPaginationHandler> _logger;


        public GetVolunteersWithPaginationHandler
            (IReadDbContext readDbContext,
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
}
