using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetMenegment.ValueObjects;


namespace PetFamily.Application.Volunteers.UpdateMainInfo.Dtos
{
    public class UpdateMainInfoDtoValidator : AbstractValidator<UpdateMainInfoDto>
    {
        public UpdateMainInfoDtoValidator()
        {
            RuleFor(u => u.FullName).MustBeValueObject(x => FullName.Create(x.Name, x.Surname, x.Patronymic));

            RuleFor(c => c.Description).MustBeValueObject(Description.Create);

            RuleFor(c => c.YearsExperience).MustBeValueObject(YearsExperience.Create);

            RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        }
    }
}
