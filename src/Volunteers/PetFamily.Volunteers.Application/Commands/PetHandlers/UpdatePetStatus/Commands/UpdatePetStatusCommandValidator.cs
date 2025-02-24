using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.UpdatePetStatus.Commands;

public class UpdatePetStatusCommandValidator : AbstractValidator<UpdatePetStatusCommand>
{
    public UpdatePetStatusCommandValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());

        RuleFor(a => a.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());

        RuleFor(a => a.AssistanceStatus).MustBeValueObject(AssistanceStatus.Create);
    }
}
