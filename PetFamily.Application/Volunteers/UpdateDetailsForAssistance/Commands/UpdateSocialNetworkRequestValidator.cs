using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Commands
{
    public class UpdateDetailsForAssistanceCommandValidator : AbstractValidator<UpdateDetailsForAssistanceCommand>
    {
        public UpdateDetailsForAssistanceCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
