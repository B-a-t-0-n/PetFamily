using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extentions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Volunteers.Application.Commands.PetHandlers.UpdatePetStatus.Commands;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.UpdatePetStatus;

public class UpdatePetStatusHandler : ICommandHandler<Guid, UpdatePetStatusCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdatePetStatusHandler> _logger;
    private readonly IValidator<UpdatePetStatusCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadVolunteersDbContext _readDbContext;


    public UpdatePetStatusHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdatePetStatusHandler> logger,
        IValidator<UpdatePetStatusCommand> validator,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork,
        IReadVolunteersDbContext readDbContext)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _readDbContext = readDbContext;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdatePetStatusCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var volunteerResult = await _volunteerRepository.GetById(
            VolunteerId.Create(command.VolunteerId), cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var assistanceStatus = AssistanceStatus.Create(command.AssistanceStatus).Value;

        var result = volunteerResult.Value.UpdatePetAssistanceStatus(PetId.Create(command.PetId), assistanceStatus);
        if (result.IsFailure)
            return result.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("updated assistance status pet with id {PetId}", command.PetId);

        return command.PetId;
    }
}
