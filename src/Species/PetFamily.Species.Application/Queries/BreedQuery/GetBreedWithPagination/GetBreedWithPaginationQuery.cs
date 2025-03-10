using PetFamily.Core.Abstractions;

namespace PetFamily.Species.Application.Queries.BreedQuery.GetBreedWithPagination;

public record GetBreedWithPaginationQuery(Guid SpeciesId, int Page, int PageSize) : IQuery;
