using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.PetManagement.Commands.PetHandlers.UpdateInfoPet.Commands;
using PetFamily.Application.PetManagement.Commands.PetHandlers.UpdateInfoPet;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Application.PetManagement.Commands.PetHandlers.UpdatePetStatus.Commands;
using PetFamily.Application.Extentions;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.UpdatePetStatus
{
    public class UpdatePetStatusHandler : ICommandHandler<Guid, UpdatePetStatusCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdatePetStatusHandler> _logger;
        private readonly IValidator<UpdatePetStatusCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReadDbContext _readDbContext;


        public UpdatePetStatusHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<UpdatePetStatusHandler> logger,
            IValidator<UpdatePetStatusCommand> validator,
            IUnitOfWork unitOfWork,
            IReadDbContext readDbContext)
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

            var petId = PetId.Create(command.PetId);

            var assistanceStatus = AssistanceStatus.Create(command.AssistanceStatus).Value;

            var pet = volunteerResult.Value.Pets.FirstOrDefault(p => p.Id == petId);
            if (pet is null)
                return Errors.General.NotFound(command.PetId).ToErrorList();

            pet.UpdateAssistanceStatus(assistanceStatus);

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("updated assistance status pet info {Nikname} with id {PetId}",
                pet.Nickname,
                command.PetId);

            return petId.Value;
        }
    }
}
