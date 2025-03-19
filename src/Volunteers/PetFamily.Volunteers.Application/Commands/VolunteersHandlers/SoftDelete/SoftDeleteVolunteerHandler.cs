using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using FluentValidation;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.SoftDelete.Commands;
using PetFamily.SharedKernel;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extentions;
using PetFamily.SharedKernel.ValueObjects.IDs;
using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.SoftDelete;

public class SoftDeleteVolunteerHandler : ICommandHandler<Guid, SoftDeleteVolunteerCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<SoftDeleteVolunteerHandler> _logger;
    private readonly IValidator<SoftDeleteVolunteerCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public SoftDeleteVolunteerHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<SoftDeleteVolunteerHandler> logger,
        IValidator<SoftDeleteVolunteerCommand> validator,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(SoftDeleteVolunteerCommand command, CancellationToken cancellationToken = default)
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

        volunteerResult.Value.Delete();
        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("soft deleted volunteer with id {id}", id.Value);

        return id.Value;
    }
}
