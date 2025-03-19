using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extentions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateMainInfo.Commands;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateMainInfo;

public class UpdateMainInfoHandler : ICommandHandler<Guid, UpdateMainInfoCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;
    private readonly IValidator<UpdateMainInfoCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;


    public UpdateMainInfoHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateMainInfoHandler> logger,
        IValidator<UpdateMainInfoCommand> validator,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateMainInfoCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var id = VolunteerId.Create(command.Id);

        var volunteerResult = await _volunteerRepository.GetById(id);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var description = Description.Create(command.Description).Value;

        var phoneNumder = PhoneNumber.Create(command.PhoneNumber).Value;

        volunteerResult.Value.UpdateMainInfo(description, phoneNumder);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("updated main info volunteer with id {id}",
            id.Value);

        return id.Value;
    }
}
