using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetMenegment.ValueObjects;

namespace PetFamily.Application.Volunteers.Create.Requests
{
    public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest> 
    {
        public CreateVolunteerRequestValidator()
        {
            RuleFor(c => c.FullName).MustBeValueObject(x => FullName.Create(x.Name, x.Surname, x.Patronymic));

            RuleFor(c => c.Description).MustBeValueObject(Description.Create);

            RuleFor(c => c.YearsExperience).MustBeValueObject(YearsExperience.Create);

            RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

            RuleForEach(c => c.DetailsForAssistance).MustBeValueObject(x => DetailsForAssistance.Create(x.Name, x.Description));

            RuleForEach(c => c.SocialNetworks).MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Link));
        }
    }
}