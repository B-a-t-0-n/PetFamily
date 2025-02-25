using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using FluentValidation;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateDetailsForAssistance.Commands;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel;
using PetFamily.Core.Extentions;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Volunteers.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateDetailsForAssistance;

public class UpdateDetailsForAssistanceHandler : ICommandHandler<Guid, UpdateDetailsForAssistanceCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateDetailsForAssistanceHandler> _logger;
    private readonly IValidator<UpdateDetailsForAssistanceCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDetailsForAssistanceHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateDetailsForAssistanceHandler> logger,
        IValidator<UpdateDetailsForAssistanceCommand> validator,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateDetailsForAssistanceCommand command, CancellationToken cancellationToken = default)
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

        var detailsForAssistanceList = new List<DetailsForAssistance>();

        if (command.DetailsForAssistance != null)
        {
            foreach (var detailsForAssistanceItem in command.DetailsForAssistance)
            {
                var detailsForAssistance = DetailsForAssistance.Create(detailsForAssistanceItem.Name, detailsForAssistanceItem.Description).Value;

                detailsForAssistanceList.Add(detailsForAssistance);
            }
        }

        volunteerResult.Value.UpdateDetailsForAssistance(detailsForAssistanceList);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("updated details for assistance volunteer {Surname} {Name} {Patronymic} with id {id}",
            volunteerResult.Value.FullName.Surname,
            volunteerResult.Value.FullName.Name,
            volunteerResult.Value.FullName.Patronymic,
            id.Value);

        return id.Value;
    }
}
