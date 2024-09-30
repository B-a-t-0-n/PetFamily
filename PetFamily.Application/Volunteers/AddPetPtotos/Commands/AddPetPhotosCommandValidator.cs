using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.AddPetPtotos.Commands
{
    public class AddPetPhotosCommandValidator : AbstractValidator<AddPetPhotosCommand>
    {
        public AddPetPhotosCommandValidator()
        {
            RuleFor(a => a.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
            RuleFor(a => a.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
