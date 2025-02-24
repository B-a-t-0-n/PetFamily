using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;

namespace PetFamily.Species.Application.Commands.BreedHandlers.RemoveBreed.Commands;

public class RemoveBreedCommandValidator : AbstractValidator<RemoveBreedCommand>
{
    public RemoveBreedCommandValidator()
    {
        RuleFor(u => u.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.BreedId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}
