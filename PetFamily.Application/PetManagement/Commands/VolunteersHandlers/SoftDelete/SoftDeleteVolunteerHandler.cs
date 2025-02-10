using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Application.Database;
using FluentValidation;
using PetFamily.Application.Extentions;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Delete.Commands;
using PetFamily.Application.Abstraction;

namespace PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Delete
{
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
            IUnitOfWork unitOfWork)
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

            _logger.LogInformation("soft deleted volunteer {Surname} {Name} {Patronymic} with id {id}",
                volunteerResult.Value.FullName.Surname,
                volunteerResult.Value.FullName.Name,
                volunteerResult.Value.FullName.Patronymic,
                id.Value);

            return id.Value;
        }
    }
}
