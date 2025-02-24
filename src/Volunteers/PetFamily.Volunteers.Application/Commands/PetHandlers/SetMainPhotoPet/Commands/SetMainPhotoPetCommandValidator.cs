using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.SetMainPhotoPet.Commands;

public class SetMainPhotoPetCommandValidator : AbstractValidator<SetMainPhotoPetCommand>
{
    public SetMainPhotoPetCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.Path).MustBeValueObject(PhotoPath.Create);
    }
}
