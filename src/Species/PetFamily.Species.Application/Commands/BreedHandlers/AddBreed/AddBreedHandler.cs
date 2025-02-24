using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extentions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Species.Application.Commands.BreedHandlers.AddBreed.Commands;
using PetFamily.Species.Domain.Entity;
using PetFamily.Species.Domain.ValueObjects;

namespace PetFamily.Species.Application.Commands.BreedHandlers.AddBreed;

public class AddBreedHandler : ICommandHandler<Guid, AddBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<AddBreedHandler> _logger;
    private readonly IValidator<AddBreedCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public AddBreedHandler(
        ISpeciesRepository speciesRepository,
        ILogger<AddBreedHandler> logger,
        IValidator<AddBreedCommand> validator,
        [FromKeyedServices(Modules.Species)] IUnitOfWork unitOfWork)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(AddBreedCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var speciesId = SpeciesId.Create(command.SpeciesId);

        var speciesResult = await _speciesRepository.GetById(speciesId, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breedId = BreedId.NewBreedId();

        var name = Name.Create(command.Name).Value;

        var breedResult = Breed.Create(breedId, name);
        if (breedResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var result = speciesResult.Value.AddBreed(breedResult.Value);
        if (result.IsFailure)
            return result.Error.ToErrorList();

        _speciesRepository.Save(speciesResult.Value, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("added breed {name} with id {breedId} species with id {speciesId}",
            name,
            breedId.Value,
            speciesId.Value);

        return (Guid)breedId.Value;
    }
}
