using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Requests
{
    public class UpdateDetailsForAssistanceRequestValidator : AbstractValidator<UpdateDetailsForAssistanceRequest>
    {
        public UpdateDetailsForAssistanceRequestValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
