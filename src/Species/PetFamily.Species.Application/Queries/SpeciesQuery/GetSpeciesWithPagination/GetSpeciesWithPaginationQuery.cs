using PetFamily.Core.Abstractions;

namespace PetFamily.Species.Application.Queries.SpeciesQuery.GetSpeciesWithPagination;

public record GetSpeciesWithPaginationQuery(int Page, int PageSize) : IQuery;
