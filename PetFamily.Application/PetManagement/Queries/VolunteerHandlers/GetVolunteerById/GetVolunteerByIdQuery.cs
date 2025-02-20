using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.Queries.VolunteerHandlers.GetVolunteerById
{
    public record GetVolunteerByIdQuery(Guid Id) : IQuery;
}
