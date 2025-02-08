using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Application.SpeciesManagment.Commands.SpeciesHandlers.Delete.Commands;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Extentions;
using Microsoft.EntityFrameworkCore;

namespace PetFamily.Application.SpeciesManagment.Commands.SpeciesHandlers.Delete
{
    public class DeleteSpeciesHandler : ICommandHandler<DeleteSpeciesCommand>
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IReadDbContext _readDbContext;
        private readonly ILogger<DeleteSpeciesHandler> _logger;
        private readonly IValidator<DeleteSpeciesCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSpeciesHandler(
            ISpeciesRepository speciesRepository,
            IReadDbContext readDbContext,
            ILogger<DeleteSpeciesHandler> logger,
            IValidator<DeleteSpeciesCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _speciesRepository = speciesRepository;
            _logger = logger;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _readDbContext = readDbContext;
        }

        public async Task<UnitResult<ErrorList>> Handle(DeleteSpeciesCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var speciesId = SpeciesId.Create(command.Id);

            var speciesResult = await _speciesRepository.GetById(speciesId);
            if (speciesResult.IsFailure)
                return speciesResult.Error.ToErrorList();

            if (await _readDbContext.Pets.AnyAsync(p => p.SpeciesId == speciesId.Value))
                return Error.Validation("record.is.used", " record is used").ToErrorList();

            _speciesRepository.Delete(speciesResult.Value, cancellationToken);

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("deleted species {Name} with id {speciesId}",
                speciesResult.Value,
                speciesId.Value);

            return Result.Success<ErrorList>();
        }
    }
}
