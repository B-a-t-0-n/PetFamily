using PetFamily.Volunteers.Application.Queries.VolunteerHandlers.GetVolunteersWithPagination;

namespace PetFamily.Volunteers.Presentation.Volunteer.Requests;

public record GetVolunteersWithPaginationRequest(int Page, int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery() =>
        new(Page, PageSize);
}
