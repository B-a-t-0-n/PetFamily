using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.UpdateMainInfo.Requests
{
    public class UpdateMainInfoRequestValidator : AbstractValidator<UpdateMainInfoRequest>
    {
        public UpdateMainInfoRequestValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
