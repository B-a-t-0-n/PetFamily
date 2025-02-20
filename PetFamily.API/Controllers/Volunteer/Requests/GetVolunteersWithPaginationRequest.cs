using PetFamily.Application.PetManagement.Queries.VolunteerHandlers.GetVolunteersWithPagination;

namespace PetFamily.API.Controllers.Volunteer.Requests
{
    public record GetVolunteersWithPaginationRequest(int Page, int PageSize)
    {
        public GetVolunteersWithPaginationQuery ToQuery() =>
            new(Page, PageSize);
    }
}
