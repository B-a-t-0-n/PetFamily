using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.RemoveBreed.Commands
{
    public class RemoveBreedCommandValidator : AbstractValidator<RemoveBreedCommand>
    {
        public RemoveBreedCommandValidator()
        {
            RuleFor(u => u.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
            RuleFor(u => u.BreedId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
