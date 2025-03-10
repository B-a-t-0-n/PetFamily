using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Species.Application.Commands.SpeciesHandlers.Delete.Commands;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Core.Extentions;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.Species.Application.Commands.SpeciesHandlers.Delete;

public class DeleteSpeciesHandler : ICommandHandler<DeleteSpeciesCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IVolunteersContract _volunteerContract;
    private readonly ILogger<DeleteSpeciesHandler> _logger;
    private readonly IValidator<DeleteSpeciesCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSpeciesHandler(
        ISpeciesRepository speciesRepository,
        IVolunteersContract volunteerContract,
        ILogger<DeleteSpeciesHandler> logger,
        IValidator<DeleteSpeciesCommand> validator,
        [FromKeyedServices(Modules.Species)] IUnitOfWork unitOfWork)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _volunteerContract = volunteerContract;
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

        var result = await _volunteerContract.DoesAnyPetHaveSpeciesWithId(speciesId);
        if (result.IsSuccess)
            return Error.Validation("record.is.used", " record is used").ToErrorList();

        _speciesRepository.Delete(speciesResult.Value, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("deleted species {Name} with id {speciesId}",
            speciesResult.Value,
            speciesId.Value);

        return Result.Success<ErrorList>();
    }
}
