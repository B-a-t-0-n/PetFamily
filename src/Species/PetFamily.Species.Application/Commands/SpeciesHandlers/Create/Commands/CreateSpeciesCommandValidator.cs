using FluentValidation;

namespace PetFamily.Species.Application.Commands.SpeciesHandlers.Create.Commands;

public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
