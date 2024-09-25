using FluentValidation;
using PetFamily.Application.Dtos;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Application.Volunteers.AddPet.Commands
{
    public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
    {
        public AddPetCommandValidator()
        {
            RuleFor(a => a.volunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());

            RuleFor(a => a.Nickname).MustBeValueObject(Nickname.Create);

            RuleFor(a => a.Description).MustBeValueObject(Description.Create);

            RuleFor(a => a.Color).MustBeValueObject(Color.Create);

            RuleFor(a => a.HealthInformation).MustBeValueObject(HealthInformation.Create);

            RuleFor(a => a.Address).MustBeValueObject(x =>
                Address.Create(x.City, x.Street, x.House, x.Flat, x.ApartmentNumber));

            RuleFor(a => a.Size).MustBeValueObject(x => Size.Create(x.Height, x.Weight));

            RuleFor(a => a.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

            RuleFor(a => a.HealthInformation).MustBeValueObject(HealthInformation.Create);

            RuleFor(a => a.IsCastrated).NotNull().WithError(Errors.General.ValueIsRequired());

            RuleFor(a => a.IsVaccinated).NotNull().WithError(Errors.General.ValueIsRequired());

            RuleForEach(c => c.DetailsForAssistance).MustBeValueObject(x => DetailsForAssistance.Create(x.Name, x.Description));
        }
    }
}
