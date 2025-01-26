using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Commands
{
    public class UpdateDetailsForAssistanceCommandValidator : AbstractValidator<UpdateDetailsForAssistanceCommand>
    {
        public UpdateDetailsForAssistanceCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
            RuleForEach(c => c.DetailsForAssistance).MustBeValueObject(x => DetailsForAssistance.Create(x.Name, x.Description));
        }
    }
}
