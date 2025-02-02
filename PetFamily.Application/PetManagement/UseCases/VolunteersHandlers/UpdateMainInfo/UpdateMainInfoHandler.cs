using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extentions;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateMainInfo.Commands;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateMainInfo
{
    public class UpdateMainInfoHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateMainInfoHandler> _logger;
        private readonly IValidator<UpdateMainInfoCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateMainInfoHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<UpdateMainInfoHandler> logger,
            IValidator<UpdateMainInfoCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(UpdateMainInfoCommand command, CancellationToken cancellationToken = default)
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

            var fullName = FullName.Create(command.FullName.Name,
                command.FullName.Surname,
                command.FullName.Patronymic).Value;

            var description = Description.Create(command.Description).Value;

            var yearsExperience = YearsExperience.Create(command.YearsExperience).Value;

            var phoneNumder = PhoneNumber.Create(command.PhoneNumber).Value;

            volunteerResult.Value.UpdateMainInfo(fullName, description, yearsExperience, phoneNumder);

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("updated main info volunteer {Surname} {Name} {Patronymic} with id {id}",
                fullName.Surname,
                fullName.Name,
                fullName.Patronymic,
                id.Value);

            return id.Value;
        }
    }
}
