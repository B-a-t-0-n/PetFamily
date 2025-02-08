using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.SpeciesManagment.Commands.SpeciesHandlers.Create.Commands;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Application.Extentions;
using PetFamily.Domain.SpeciesMenegment.ValueObjects;
using PetFamily.Domain.SpeciesMenegment.Entity;
using Microsoft.EntityFrameworkCore;

namespace PetFamily.Application.SpeciesManagment.Commands.SpeciesHandlers.Create
{
    public class CreateSpeciesHandler : ICommandHandler<Guid, CreateSpeciesCommand>
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IReadDbContext _readDbContext;
        private readonly ILogger<CreateSpeciesHandler> _logger;
        private readonly IValidator<CreateSpeciesCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSpeciesHandler(
            ISpeciesRepository speciesRepository,
            IReadDbContext readDbContext,
            ILogger<CreateSpeciesHandler> logger,
            IValidator<CreateSpeciesCommand> validator,
            IUnitOfWork unitOfWork) 
        {
            _speciesRepository = speciesRepository;
            _logger = logger;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _readDbContext = readDbContext;
        }

        public async Task<Result<Guid, ErrorList>> Handle(CreateSpeciesCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var speciesId = SpeciesId.NewSpeciesId();

            var name = Name.Create(command.Name).Value;

            var speciesResult = Species.Create(speciesId, name);
            if (speciesResult.IsFailure)
                return speciesResult.Error.ToErrorList();

            if (await _readDbContext.Species.AnyAsync(s => s.Name.ToLower() == name.Value.ToLower()))
                return Errors.General.AlreadyExist().ToErrorList();

            await _speciesRepository.Add(speciesResult.Value, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("created species {Name} with id {speciesId}",
                name,
                speciesId.Value);

            return (Guid)speciesResult.Value.Id;
        }
    }
}
