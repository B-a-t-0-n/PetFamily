using FluentValidation;

namespace PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.AddBreed.Commands
{
    public class AddBreedCommandValidator : AbstractValidator<AddBreedCommand>
    {
        public AddBreedCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
