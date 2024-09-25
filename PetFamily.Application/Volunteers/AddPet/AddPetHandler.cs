using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Infrastucture.Repositories;
using PetFamily.Domain.Shared;
using PetFamily.Application.Volunteers.AddPet.Commands;
using PetFamily.Application.Providers;

namespace PetFamily.Application.Volunteers.AddPet
{
    public class AddPetHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<AddPetHandler> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AddPetHandler(IVolunteerRepository volunteerRepository, ILogger<AddPetHandler> logger, IDateTimeProvider dateTimeProvider)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<Guid, Error>> Handle(AddPetCommand command, CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteerRepository.GetById(
                VolunteerId.Create(command.volunteerId), cancellationToken);

            if(volunteerResult.IsFailure)
                return volunteerResult.Error;

            var petId = PetId.NewPetId();

            var nickname = Nickname.Create(command.Nickname).Value;

            var speciesId = SpeciesId.Create(command.SpeciesAndBreed.SpeciesId);
            var speciesAndBreed = SpeciesAndBreed.Create(speciesId, command.SpeciesAndBreed.BreedId).Value;
            
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
                    var value = DetailsForAssistance.Create(detailsForAssistance.Name, detailsForAssistance.Description).Value;

                    detailsForAssistances.Add(value);
                }
            }

            var petDetailsForAssistance = new PetDetailsForAssistance(detailsForAssistances);

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
                petDetailsForAssistance);

            if (petResult.IsFailure)
                return petResult.Error;

            volunteerResult.Value.AddPet(petResult.Value);

            await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("added pet {Nickname} with id {petId} volunteer with id {volunteerId}",
                nickname,
                petId.Value,
                volunteerResult.Value.Id);

            return (Guid)petResult.Value.Id;
        }
    }
}
