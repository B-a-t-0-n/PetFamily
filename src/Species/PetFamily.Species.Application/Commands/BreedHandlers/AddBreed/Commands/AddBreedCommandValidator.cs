using FluentValidation;

namespace PetFamily.Species.Application.Commands.BreedHandlers.AddBreed.Commands;

public class AddBreedCommandValidator : AbstractValidator<AddBreedCommand>
{
    public AddBreedCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
