using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Extentions;
using PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.AddBreed.Commands;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.SpeciesMenegment.Entity;
using PetFamily.Domain.SpeciesMenegment.ValueObjects;
using System.Threading;

namespace PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.AddBreed
{
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
            IUnitOfWork unitOfWork)
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
}
