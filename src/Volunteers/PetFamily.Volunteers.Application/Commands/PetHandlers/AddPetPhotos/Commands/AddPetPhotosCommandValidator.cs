using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.AddPetPhotos.Commands;

public class AddPetPhotosCommandValidator : AbstractValidator<AddPetPhotosCommand>
{
    public AddPetPhotosCommandValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(a => a.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}
