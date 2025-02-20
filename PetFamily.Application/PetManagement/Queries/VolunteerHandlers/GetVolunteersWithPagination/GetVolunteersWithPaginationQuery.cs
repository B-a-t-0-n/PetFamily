using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.Queries.VolunteerHandlers.GetVolunteersWithPagination
{
    public record GetVolunteersWithPaginationQuery(int Page, int PageSize) : IQuery;
}
