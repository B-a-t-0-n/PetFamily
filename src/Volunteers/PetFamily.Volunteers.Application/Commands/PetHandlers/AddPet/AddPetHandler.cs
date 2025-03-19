using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using FluentValidation;
using PetFamily.Volunteers.Application.Commands.PetHandlers.AddPet.Commands;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel;
using PetFamily.Core.Extentions;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.ValueObjects;
using PetFamily.Volunteers.Domain.Entity;
using PetFamily.Species.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.AddPet;

public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesContract _speciesContract;

    public AddPetHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<AddPetHandler> logger,
        IDateTimeProvider dateTimeProvider,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork,
        IValidator<AddPetCommand> validator,
        ISpeciesContract speciesContract)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _speciesContract = speciesContract;
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

        var speciesResult = await _speciesContract.GetSpeciesById(command.SpeciesAndBreed.SpeciesId);
        if (speciesResult.IsFailure)
            return speciesResult.Error;

        var breedResult = await _speciesContract.GetBreedById(
            command.SpeciesAndBreed.SpeciesId,
            command.SpeciesAndBreed.BreedId);
        if (breedResult.IsFailure)
            return breedResult.Error;

        var speciesAndBreed = SpeciesAndBreed.Create(SpeciesId.Create(speciesResult.Value.Id), breedResult.Value.Id).Value;

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

        var detailsForAssistances = new List<Requisites>();

        if (command.DetailsForAssistance != null)
        {
            foreach (var detailsForAssistance in command.DetailsForAssistance)
            {
                var value = Requisites.Create(
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
