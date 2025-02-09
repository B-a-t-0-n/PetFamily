using FluentValidation;

namespace PetFamily.Application.SpeciesManagment.Commands.SpeciesHandlers.Create.Commands
{
    public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
    {
        public CreateSpeciesCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
