using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extentions;
using PetFamily.Application.Models;
using System.Linq.Expressions;

namespace PetFamily.Application.PetManagement.Queries.PetHandlers.GetPetWithPaginationFiltration
{
    public class GetPetWithPaginationFiltrationHandler : IQueryHandler<PagedList<PetDto>, GetPetWithPaginationFiltrationQuery>
    {
        private readonly IReadDbContext _readDbContext;
        private readonly ILogger<GetPetWithPaginationFiltrationHandler> _logger;


        public GetPetWithPaginationFiltrationHandler(
            IReadDbContext readDbContext,
            ILogger<GetPetWithPaginationFiltrationHandler> logger)
        {
            _readDbContext = readDbContext;
            _logger = logger;
        }

        public async Task<PagedList<PetDto>> Handle(
            GetPetWithPaginationFiltrationQuery query,
            CancellationToken cancellationToken = default)
        {
            var petQuery = _readDbContext.Pets;

            petQuery = petQuery
                .WhereIf(
                    query.VolunteerId.HasValue,
                    x => x.VolunteerId == query.VolunteerId)
                .WhereIf(
                    !string.IsNullOrEmpty(query.Nickname),
                    x => x.Nickname.Contains(query.Nickname!))
                .WhereIf(
                    query.SpeciesId.HasValue,
                    x => x.SpeciesId == query.SpeciesId)
                .WhereIf(
                    query.BreedId.HasValue,
                    x => x.BreedId == query.BreedId)
                .WhereIf(
                    !string.IsNullOrEmpty(query.Color),
                    x => x.Color.ToLower() == query.Color!.ToLower())
                .WhereIf(
                    query.IsCastrated.HasValue,
                    x => x.IsCastrated == query.IsCastrated)
                .WhereIf(
                    query.DateOfBirth.HasValue,
                    x => x.DateOfBirth == query.DateOfBirth)
                .WhereIf(
                    query.IsVaccinated.HasValue,
                    x => x.IsVaccinated == query.IsVaccinated)
                .WhereIf(
                    !string.IsNullOrEmpty(query.AssistanceStatus),
                    x => x.AssistanceStatus.ToLower() == query.AssistanceStatus!.ToLower())
                .WhereIf(
                    !string.IsNullOrEmpty(query.Сity),
                    x => x.City.ToLower() == query.Сity!.ToLower())
                .WhereIf(
                    !string.IsNullOrEmpty(query.Street),
                    x => x.Street.ToLower() == query.Street!.ToLower())
                .WhereIf(
                    !string.IsNullOrEmpty(query.House),
                    x => x.House.ToLower() == query.House!.ToLower( ))
                .WhereIf(
                    !string.IsNullOrEmpty(query.Flat),
                    x => x.Flat.ToLower() == query.Flat!.ToLower())
                .WhereIf(
                    query.SerialNumberTo != null,
                    x => x.SerialNumber <= query.SerialNumberTo!.Value)
                .WhereIf(
                    query.SerialNumberFrom != null,
                    x => x.SerialNumber >= query.SerialNumberFrom!.Value);

            Expression<Func<PetDto, object>> keySelector = query.SortBy?.ToLower() switch
            {
                "nickname" => x => x.Nickname,
                "species" => x => x.SpeciesId,
                "breed" => x => x.SpeciesId,
                "color" => x => x.Color,
                "iscastrated" => x => x.IsCastrated,
                "dateofbirth" => x => x.DateOfBirth,
                "isvaccinated" => x => x.IsVaccinated,
                "assistancestatus" => x => x.AssistanceStatus,
                "city" => x => x.City,
                "street" => x => x.Street,
                "house" => x => x.House,
                "flat" => x => x.Flat,
                "volunteer" => x => x.VolunteerId,
                _ => x => x.Id
            };

            petQuery = query.SortDirection?.ToLower() switch
            {
                "asc" => petQuery.OrderBy(keySelector),
                "desc" => petQuery.OrderByDescending(keySelector),
                _ => petQuery
            };

            var pagedList = await petQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

            _logger.LogInformation("received pets with page {page} of page size {PageSize}",
                query.Page,
                query.PageSize);

            return pagedList;
        }
    }
}
