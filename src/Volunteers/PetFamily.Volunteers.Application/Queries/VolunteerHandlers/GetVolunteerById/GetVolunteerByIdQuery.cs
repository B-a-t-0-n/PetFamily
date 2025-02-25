using PetFamily.Core.Abstractions;

namespace PetFamily.Volunteers.Application.Queries.VolunteerHandlers.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;
