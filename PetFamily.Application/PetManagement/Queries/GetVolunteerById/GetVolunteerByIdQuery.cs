using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.Queries.GetVolunteerById
{
    public record GetVolunteerByIdQuery(Guid Id) : IQuery;
}
