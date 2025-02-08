using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Extentions;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Create.Commands;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Create
{
    public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand> 
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<CreateVolunteerHandler> _logger;
        private readonly IValidator<CreateVolunteerCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReadDbContext _readDbContext;


        public CreateVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<CreateVolunteerHandler> logger,
            IValidator<CreateVolunteerCommand> validator,
            IUnitOfWork unitOfWork,
            IReadDbContext readDbContext)
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

            var fullName = FullName.Create(command.FullName.Name, command.FullName.Surname, command.FullName.Patronymic).Value;

            var description = Description.Create(command.Description).Value;

            var yearsExperience = YearsExperience.Create(command.YearsExperience).Value;

            var phoneNumder = PhoneNumber.Create(command.PhoneNumber).Value;

            var detailsForAssistances = new List<DetailsForAssistance>();

            if (command.DetailsForAssistance != null)
            {
                foreach (var detailsForAssistance in command.DetailsForAssistance)
                {
                    var value = DetailsForAssistance.Create(detailsForAssistance.Name, detailsForAssistance.Description).Value;

                    detailsForAssistances.Add(value);
                }
            }

            var socialNetworks = new List<SocialNetwork>();

            if (command.SocialNetworks != null)
            {
                foreach (var socialnetwork in command.SocialNetworks)
                {
                    var socialNetwork = SocialNetwork.Create(socialnetwork.Name, socialnetwork.Link).Value;

                    socialNetworks.Add(socialNetwork);
                }
            }

            var volunteerResult = Volunteer.Create(volunteerId,
                fullName,
                description,
                yearsExperience,
                phoneNumder,
                detailsForAssistances,
                socialNetworks);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            if (await _readDbContext.Volunteers.AnyAsync(v => v.PhoneNumber == phoneNumder.Number))
                return Errors.General.AlreadyExist().ToErrorList();

            await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("created volunteer {Surname} {Name} {Patronymic} with id {volunteerId}",
                fullName.Surname,
                fullName.Name,
                fullName.Patronymic,
                volunteerId.Value);

            return (Guid)volunteerResult.Value.Id;
        }
    }
}
