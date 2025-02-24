using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateDetailsForAssistance.Commands;

public class UpdateDetailsForAssistanceCommandValidator : AbstractValidator<UpdateDetailsForAssistanceCommand>
{
    public UpdateDetailsForAssistanceCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(c => c.DetailsForAssistance).MustBeValueObject(x => DetailsForAssistance.Create(x.Name, x.Description));
    }
}
