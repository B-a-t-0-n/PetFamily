using FluentValidation;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Core.Validation;

namespace PetFamily.Accounts.Application.Commands.Register.Command;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(u => u.FullName).MustBeValueObject(x => FullName.Create(x.Name, x.Surname, x.Patronymic));

        RuleForEach(c => c.SocialNetworks).MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Link));

        RuleFor(u => u.Email).NotEmpty().EmailAddress();

        RuleFor(u => u.UserName).NotEmpty();

        
    }
}