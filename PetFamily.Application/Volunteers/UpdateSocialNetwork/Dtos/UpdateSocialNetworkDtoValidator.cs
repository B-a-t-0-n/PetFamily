using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetMenegment.ValueObjects;


namespace PetFamily.Application.Volunteers.UpdateSocialNetwork.Dtos
{
    public class UpdateSocialNetworkDtoValidator : AbstractValidator<UpdateSocialNetworkDto>
    {
        public UpdateSocialNetworkDtoValidator()
        {
            RuleForEach(c => c.SocialNetwork).MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Link));
        }
    }
}
