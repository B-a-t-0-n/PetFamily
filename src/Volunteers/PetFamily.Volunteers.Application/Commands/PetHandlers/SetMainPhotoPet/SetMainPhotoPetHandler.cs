using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extentions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Volunteers.Application.Commands.PetHandlers.SetMainPhotoPet.Commands;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.SetMainPhotoPet;

public class SetMainPhotoPetHandler : ICommandHandler<SetMainPhotoPetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<SetMainPhotoPetHandler> _logger;
    private readonly IValidator<SetMainPhotoPetCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public SetMainPhotoPetHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<SetMainPhotoPetHandler> logger,
        IValidator<SetMainPhotoPetCommand> validator,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(SetMainPhotoPetCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var id = VolunteerId.Create(command.VolunteerId);

        var volunteerResult = await _volunteerRepository.GetById(id);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var pathResult = PhotoPath.Create(command.Path);
        if (pathResult.IsFailure)
            return pathResult.Error.ToErrorList();

        var result = volunteerResult.Value.SetMainPhotoPet(PetId.Create(command.PetId), pathResult.Value);
        if (result.IsFailure)
            return result.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("set new main photo pet with id {id}",
            command.PetId);

        return Result.Success<ErrorList>();
    }
}
