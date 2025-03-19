using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extentions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.Create.Commands;
using PetFamily.Volunteers.Domain.Entity;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.Create;

public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand> 
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadVolunteersDbContext _readDbContext;


    public CreateVolunteerHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<CreateVolunteerHandler> logger,
        IValidator<CreateVolunteerCommand> validator,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork,
        IReadVolunteersDbContext readDbContext)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _readDbContext = readDbContext;
    }

    public async Task<Result<Guid, ErrorList>> Handle(CreateVolunteerCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var volunteerId = VolunteerId.NewVolunteerId();

        var description = Description.Create(command.Description).Value;

        var phoneNumder = PhoneNumber.Create(command.PhoneNumber).Value;

        var volunteerResult = Volunteer.Create(
            volunteerId,
            description,
            phoneNumder);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        if (await _readDbContext.Volunteers.AnyAsync(v => v.PhoneNumber == phoneNumder.Number, cancellationToken: cancellationToken))
            return Errors.General.AlreadyExist().ToErrorList();

        await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("created volunteer with id {volunteerId}", volunteerId.Value);

        return (Guid)volunteerResult.Value.Id;
    }
}
