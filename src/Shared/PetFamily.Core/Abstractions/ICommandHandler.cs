using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Core.Abstractions;

public interface ICommandHandler<TResponce, in TCommand> where TCommand : ICommand
{
    Task<Result<TResponce, ErrorList>> Handle(TCommand command, CancellationToken cancellation = default);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<UnitResult<ErrorList>> Handle(TCommand command, CancellationToken cancellation = default);
}
