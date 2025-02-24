using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateSocialNetwork.Commands;

public class UpdateSocialNetworkCommandValidator : AbstractValidator<UpdateSocialNetworkCommand>
{
    public UpdateSocialNetworkCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(c => c.SocialNetwork).MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Link));
    }
}
