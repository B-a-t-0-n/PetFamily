using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.PetManagement.UseCases.PetHandlers.AddPetPhotos.Commands
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
