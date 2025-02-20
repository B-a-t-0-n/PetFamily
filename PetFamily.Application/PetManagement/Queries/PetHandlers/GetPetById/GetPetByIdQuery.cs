using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.Queries.PetHandlers.GetPetById
{
    public record GetPetByIdQuery(Guid Id) : IQuery;
}

