using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.UpdatePetStatus.Commands
{
    public class UpdatePetStatusCommandValidator : AbstractValidator<UpdatePetStatusCommand>
    {
        public UpdatePetStatusCommandValidator()
        {
            RuleFor(a => a.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());

            RuleFor(a => a.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());

            RuleFor(a => a.AssistanceStatus).MustBeValueObject(AssistanceStatus.Create);
        }
    }
}
