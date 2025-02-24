using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Volunteers.Application.Commands.PetHandlers.UpdateInfoPet.Commands;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel;
using PetFamily.Core.Extentions;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.ValueObjects;
using PetFamily.Species.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.UpdateInfoPet;

public class UpdatePetInfoHandler : ICommandHandler<Guid, UpdatePetInfoCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdatePetInfoHandler> _logger;
    private readonly IValidator<UpdatePetInfoCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesContract _speciesContract;


    public UpdatePetInfoHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdatePetInfoHandler> logger,
        IValidator<UpdatePetInfoCommand> validator,
       [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork,
        ISpeciesContract speciesContract)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _speciesContract = speciesContract;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdatePetInfoCommand command, CancellationToken cancellationToken = default)
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

        var petId = PetId.Create(command.PetId);

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

        var result = volunteerResult.Value.UpdatePetInfo(
            PetId.Create(command.PetId),
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
            detailsForAssistances);
        if(result.IsFailure)
            return result.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("updated pet info {Nikname} with id {PetId}",
            command.Nickname,
            command.PetId);

        return petId.Value;
    }
}
