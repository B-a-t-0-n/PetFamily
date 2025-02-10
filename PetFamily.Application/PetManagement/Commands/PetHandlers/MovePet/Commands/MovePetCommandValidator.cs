using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.PetManagement.UseCases.PetHandlers.MovePet.Commands
{
    public class MovePetCommandValidator : AbstractValidator<MovePetCommand>
    {
        public MovePetCommandValidator()
        {
            RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
            RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
            RuleFor(u => u.SerialNumber).MustBeValueObject(SerialNumber.Create);
        }
    }
}
