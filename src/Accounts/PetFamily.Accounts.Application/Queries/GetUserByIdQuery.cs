using PetFamily.Core.Abstractions;

namespace PetFamily.Accounts.Application.Queries;

public record GetUserByIdQuery(Guid Id) : IQuery;
