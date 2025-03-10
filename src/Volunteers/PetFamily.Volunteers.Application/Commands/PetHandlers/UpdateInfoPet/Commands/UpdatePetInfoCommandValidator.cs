using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.PetHandlers.UpdateInfoPet.Commands;

public class UpdatePetInfoCommandValidator : AbstractValidator<UpdatePetInfoCommand>
{
    public UpdatePetInfoCommandValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());

        RuleFor(a => a.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());

        RuleFor(a => a.Nickname).MustBeValueObject(Nickname.Create);

        RuleFor(a => a.Description).MustBeValueObject(Description.Create);

        RuleFor(a => a.Color).MustBeValueObject(Color.Create);

        RuleFor(a => a.HealthInformation).MustBeValueObject(HealthInformation.Create);

        RuleFor(a => a.Address).MustBeValueObject(x =>
            Address.Create(x.City, x.Street, x.House, x.Flat, x.ApartmentNumber));

        RuleFor(a => a.Size).MustBeValueObject(x => Size.Create(x.Height, x.Weight));

        RuleFor(a => a.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleFor(a => a.AssistanceStatus).MustBeValueObject(AssistanceStatus.Create);

        RuleFor(a => a.IsCastrated).NotNull().WithError(Errors.General.ValueIsRequired());

        RuleFor(a => a.IsVaccinated).NotNull().WithError(Errors.General.ValueIsRequired());

        RuleForEach(c => c.DetailsForAssistance).MustBeValueObject(x => DetailsForAssistance.Create(x.Name, x.Description));
    }
}
