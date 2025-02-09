using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Delete.Commands
{
    public class SoftDeleteVolunteerCommandValidator : AbstractValidator<SoftDeleteVolunteerCommand>
    {
        public SoftDeleteVolunteerCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
