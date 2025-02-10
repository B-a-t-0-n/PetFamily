using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Application.Extentions;
using PetFamily.Application.PetManagement.Commands.PetHandlers.SoftDeletePet.Commands;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.SoftDeletePet
{
    public class SoftDeletePetHandler : ICommandHandler<Guid, SoftDeletePetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<SoftDeletePetHandler> _logger;
        private readonly IValidator<SoftDeletePetCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public SoftDeletePetHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<SoftDeletePetHandler> logger,
            IValidator<SoftDeletePetCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(SoftDeletePetCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var volunteerid = VolunteerId.Create(command.VolunteerId);

            var volunteerResult = await _volunteerRepository.GetById(volunteerid);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var result = volunteerResult.Value.SoftDeletePet(PetId.Create(command.PetId));
            if(result.IsFailure)
                return result.Error.ToErrorList();

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("soft deleted pet with id {id}",
                command.PetId);

            return command.PetId;
        }
    }
}
