using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.SetMainPhotoPet.Commands
{
    public class SetMainPhotoPetCommandValidator : AbstractValidator<SetMainPhotoPetCommand>
    {
        public SetMainPhotoPetCommandValidator()
        {
            RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
            RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
            RuleFor(u => u.Path).MustBeValueObject(PhotoPath.Create);
        }
    }
}
