using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Extentions;
using PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.RemoveBreed.Commands;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.RemoveBreed
{
    public class RemoveBreedHandler : ICommandHandler<RemoveBreedCommand>
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IReadDbContext _readDbContext;
        private readonly ILogger<RemoveBreedHandler> _logger;
        private readonly IValidator<RemoveBreedCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveBreedHandler(
            ISpeciesRepository speciesRepository,
            IReadDbContext readDbContext,
            ILogger<RemoveBreedHandler> logger,
            IValidator<RemoveBreedCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _speciesRepository = speciesRepository;
            _logger = logger;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _readDbContext = readDbContext;
        }

        public async Task<UnitResult<ErrorList>> Handle(RemoveBreedCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var speciesId = SpeciesId.Create(command.SpeciesId);

            var speciesResult = await _speciesRepository.GetById(speciesId);
            if (speciesResult.IsFailure)
                return speciesResult.Error.ToErrorList();

            var breed = speciesResult.Value.breeds.FirstOrDefault(b => b.Id == command.BreedId);
            if (breed is null)
                return Errors.General.NotFound().ToErrorList();

            if (await _readDbContext.Pets.AnyAsync(p => p.SpeciesId == speciesId && p.BreedId == breed.Id))
                return Error.Validation("record.is.used", " record is used").ToErrorList();

            speciesResult.Value.RemoveBreed(breed);

            _speciesRepository.Save(speciesResult.Value, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("removed breed {breedName} with id {breedId} in species {speciesName} with id {speciesId}",
                breed.Name,
                breed.Id,
                speciesResult.Value,
                speciesId.Value);

            return Result.Success<ErrorList>();
        }
    }
}
