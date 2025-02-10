using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.PetManagement.Commands.VolunteersHandlers.HardDelete.Commands
{
    public class HardDeleteVolunteerCommandValidator : AbstractValidator<HardDeleteVolunteerCommand>
    {
        public HardDeleteVolunteerCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
