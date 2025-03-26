using PetFamily.Core.Abstractions;

namespace PetFamily.Accounts.Application.Commands.Login.Command;

public record LoginCommand(string Email, string Password) : ICommand;