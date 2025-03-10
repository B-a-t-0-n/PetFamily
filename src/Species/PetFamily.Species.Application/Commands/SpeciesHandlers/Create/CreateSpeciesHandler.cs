using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using PetFamily.Species.Application.Commands.SpeciesHandlers.Create.Commands;
using PetFamily.SharedKernel;
using PetFamily.Species.Domain.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Core.Extentions;
using PetFamily.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.Species.Application.Commands.SpeciesHandlers.Create;

public class CreateSpeciesHandler : ICommandHandler<Guid, CreateSpeciesCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IReadSpeciesDbContext _readDbContext;
    private readonly ILogger<CreateSpeciesHandler> _logger;
    private readonly IValidator<CreateSpeciesCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSpeciesHandler(
        ISpeciesRepository speciesRepository,
        IReadSpeciesDbContext readDbContext,
        ILogger<CreateSpeciesHandler> logger,
        IValidator<CreateSpeciesCommand> validator,
        [FromKeyedServices(Modules.Species)] IUnitOfWork unitOfWork) 
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

        var speciesResult = Domain.Entity.Species.Create(speciesId, name);
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
