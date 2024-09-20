using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.Delete.Requests
{
    public class DeleteVolunteerRequestValidator : AbstractValidator<DeleteVolunteerRequest>
    {
        public DeleteVolunteerRequestValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
