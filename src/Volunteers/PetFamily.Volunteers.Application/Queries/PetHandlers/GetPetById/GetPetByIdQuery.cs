using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Queries.PetHandlers.GetPetById;

public record GetPetByIdQuery(Guid Id) : IQuery;
