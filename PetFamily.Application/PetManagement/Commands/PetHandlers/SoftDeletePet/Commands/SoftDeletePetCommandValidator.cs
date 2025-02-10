using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.SoftDeletePet.Commands
{
    public class SoftDeletePetCommandValidator : AbstractValidator<SoftDeletePetCommand>
    {
        public SoftDeletePetCommandValidator()
        {
            RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
            RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());        }
    }
}
