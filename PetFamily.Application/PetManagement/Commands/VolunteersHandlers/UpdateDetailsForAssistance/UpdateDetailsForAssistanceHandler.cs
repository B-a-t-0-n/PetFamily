using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Application.Database;
using FluentValidation;
using PetFamily.Application.Extentions;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateDetailsForAssistance.Commands;
using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateDetailsForAssistance
{
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
            IUnitOfWork unitOfWork)
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
}
