using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.HardDeletePet.Commands
{
    public class HardDeletePetCommandValidator : AbstractValidator<HardDeletePetCommand>
    {
        public HardDeletePetCommandValidator()
        {
            RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
            RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
