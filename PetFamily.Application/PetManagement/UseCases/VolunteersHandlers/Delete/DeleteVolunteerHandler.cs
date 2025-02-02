using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Application.Database;
using FluentValidation;
using PetFamily.Application.Extentions;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Delete.Commands;

namespace PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Delete
{
    public class DeleteVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<DeleteVolunteerHandler> _logger;
        private readonly IValidator<DeleteVolunteerCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<DeleteVolunteerHandler> logger,
            IValidator<DeleteVolunteerCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(DeleteVolunteerCommand command, CancellationToken cancellationToken = default)
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

            _logger.LogInformation("deleted volunteer {Surname} {Name} {Patronymic} with id {id}",
                volunteerResult.Value.FullName.Surname,
                volunteerResult.Value.FullName.Name,
                volunteerResult.Value.FullName.Patronymic,
                id.Value);

            return id.Value;
        }
    }
}
