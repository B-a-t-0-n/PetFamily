using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Application.Providers;
using PetFamily.Application.Database;
using FluentValidation;
using PetFamily.Application.Extentions;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.AddPet.Commands;
using PetFamily.Application.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace PetFamily.Application.PetManagement.UseCases.PetHandlers.AddPet
{
    public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<AddPetHandler> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IValidator<AddPetCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReadDbContext _readDbContext;

        public AddPetHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<AddPetHandler> logger,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork,
            IValidator<AddPetCommand> validator,
            IReadDbContext readDbContext)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _readDbContext = readDbContext;
        }

        public async Task<Result<Guid, ErrorList>> Handle(AddPetCommand command, CancellationToken cancellationToken = default)
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

            var petId = PetId.NewPetId();

            var nickname = Nickname.Create(command.Nickname).Value;

            var species = await _readDbContext.Species
                .FirstOrDefaultAsync(s => s.Id == command.SpeciesAndBreed.SpeciesId, cancellationToken);
            if (species is null)
                return Errors.General.NotFound(command.SpeciesAndBreed.SpeciesId).ToErrorList();

            var breed = await _readDbContext.Breeds
                .FirstOrDefaultAsync(b => b.Id == command.SpeciesAndBreed.BreedId);
            if (breed is null)
                return Errors.General.NotFound(command.SpeciesAndBreed.BreedId).ToErrorList();

            var speciesAndBreed = SpeciesAndBreed.Create(SpeciesId.Create(species.Id), breed.Id).Value;

            var description = Description.Create(command.Description).Value;

            var color = Color.Create(command.Color).Value;

            var healthInformation = HealthInformation.Create(command.HealthInformation).Value;

            var address = Address.Create(
                command.Address.City,
                command.Address.Street,
                command.Address.House,
                command.Address.Flat,
                command.Address.ApartmentNumber).Value;

            var size = Size.Create(command.Size.Height, command.Size.Weight).Value;

            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

            var assistanceStatus = AssistanceStatus.Create(command.AssistanceStatus).Value;

            var detailsForAssistances = new List<DetailsForAssistance>();

            if (command.DetailsForAssistance != null)
            {
                foreach (var detailsForAssistance in command.DetailsForAssistance)
                {
                    var value = DetailsForAssistance.Create(
                        detailsForAssistance.Name,
                        detailsForAssistance.Description).Value;

                    detailsForAssistances.Add(value);
                }
            }

            var petResult = Pet.Create(
                petId,
                nickname,
                speciesAndBreed,
                description,
                color,
                healthInformation,
                address,
                size,
                phoneNumber,
                command.IsCastrated,
                command.DateOfBirth,
                command.IsVaccinated,
                assistanceStatus,
                _dateTimeProvider.UtcNow,
                detailsForAssistances);

            if (petResult.IsFailure)
                return petResult.Error.ToErrorList();

            volunteerResult.Value.AddPet(petResult.Value);

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("added pet {Nickname} with id {petId} volunteer with id {volunteerId}",
                nickname,
                petId.Value,
                volunteerResult.Value.Id);

            return (Guid)petResult.Value.Id;
        }
    }
}
