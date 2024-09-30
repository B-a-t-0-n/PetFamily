using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.AddPet.Commands
{
    public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
    {
        public AddPetCommandValidator()
        {
            RuleFor(a => a.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
