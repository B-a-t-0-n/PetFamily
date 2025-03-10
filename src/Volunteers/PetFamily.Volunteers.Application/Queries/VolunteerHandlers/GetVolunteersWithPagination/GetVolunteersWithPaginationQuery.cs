using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Queries.VolunteerHandlers.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(int Page, int PageSize) : IQuery;
