using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.Volunteers.Delete.Commands;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.DeletePetPhoto.Commands
{
    public class DeletePetPhotoCommandValidator : AbstractValidator<DeletePetPhotoCommand>
    {
        public DeletePetPhotoCommandValidator()
        {
            RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
            RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
            RuleFor(u => u.PetPhotoId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
