using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetMenegment.ValueObjects;

namespace PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Dtos
{
    public class UpdateDetailsForAssistanceDtoValidator : AbstractValidator<UpdateDetailsForAssistanceDto>
    {
        public UpdateDetailsForAssistanceDtoValidator()
        {
            RuleForEach(c => c.DetailsForAssistance).MustBeValueObject(x => DetailsForAssistance.Create(x.Name, x.Description));
        }
    }
}
