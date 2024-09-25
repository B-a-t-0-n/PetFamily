using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork.Commands
{
    public class UpdateSocialNetworkCommandValidator : AbstractValidator<UpdateSocialNetworkCommand>
    {
        public UpdateSocialNetworkCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
