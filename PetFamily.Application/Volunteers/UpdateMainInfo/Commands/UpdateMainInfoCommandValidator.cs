using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.UpdateMainInfo.Commands
{
    public class UpdateMainInfoCommandValidator : AbstractValidator<UpdateMainInfoCommand>
    {
        public UpdateMainInfoCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());

            RuleFor(u => u.FullName).MustBeValueObject(x => FullName.Create(x.Name, x.Surname, x.Patronymic));

            RuleFor(c => c.Description).MustBeValueObject(Description.Create);

            RuleFor(c => c.YearsExperience).MustBeValueObject(YearsExperience.Create);

            RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        }
    }
}
