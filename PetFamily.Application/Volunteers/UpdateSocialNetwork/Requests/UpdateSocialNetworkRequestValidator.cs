using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork.Requests
{
    public class UpdateSocialNetworkRequestValidator : AbstractValidator<UpdateSocialNetworkRequest>
    {
        public UpdateSocialNetworkRequestValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
