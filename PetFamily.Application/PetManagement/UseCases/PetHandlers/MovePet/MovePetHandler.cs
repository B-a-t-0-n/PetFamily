using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.Shared;
using PetFamily.Application.Extentions;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.MovePet.Commands;

namespace PetFamily.Application.PetManagement.UseCases.PetHandlers.MovePet
{
    public class MovePetHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<MovePetHandler> _logger;
        private readonly IValidator<MovePetCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public MovePetHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<MovePetHandler> logger,
            IValidator<MovePetCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<int, ErrorList>> Handle(MovePetCommand command, CancellationToken cancellationToken = default)
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

            var pet = volunteerResult.Value.Pets.FirstOrDefault(p => p.Id == command.PetId);
            if (pet is null)
                return Errors.General.NotFound(command.PetId).ToErrorList();

            var serialNumberResult = SerialNumber.Create(command.SerialNumber);
            if (serialNumberResult.IsFailure)
                return serialNumberResult.Error.ToErrorList();

            var result = volunteerResult.Value.MovePet(pet, serialNumberResult.Value);
            if (result.IsFailure)
                return result.Error.ToErrorList();

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("move pet with id {id}",
                command.PetId);

            return pet.SerialNumber.Value;
        }
    }
}
