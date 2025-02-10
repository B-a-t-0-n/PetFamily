using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Application.PetManagement.Commands.PetHandlers.SetMainPhotoPet.Commands;
using PetFamily.Application.Extentions;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.SetMainPhotoPet
{
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
            IUnitOfWork unitOfWork)
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
}
