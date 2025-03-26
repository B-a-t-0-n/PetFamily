using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Application.Commands.Register.Command;
using PetFamily.Accounts.Application.Managers;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extentions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Application.Commands.Register;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IAccountsManager _accountsManager;
    private readonly ILogger<RegisterUserHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<RegisterUserCommand> _validator;

    public RegisterUserHandler(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IAccountsManager accountsManager,
        ILogger<RegisterUserHandler> logger,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork,
        IValidator<RegisterUserCommand> validator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _accountsManager = accountsManager;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var role = _roleManager.FindByNameAsync(PartisipantAccount.PARTISIPANT).Result;
            if (role is null)
            {
                return Error.Failure("role.failed", "role PARTICIPANT not found").ToErrorList();
            }

            var fullNameResult = FullName.Create(
                command.FullName.Name,
                command.FullName.Surname,
                command.FullName.Patronymic);
            if (fullNameResult.IsFailure)
                return fullNameResult.Error.ToErrorList();

            var socialNetworks = new List<SocialNetwork>();
            foreach (var socialNetwork in command.SocialNetworks ?? [])
            {
                var socialNetworkResult = SocialNetwork.Create(socialNetwork.Name, socialNetwork.Link);
                if (socialNetworkResult.IsFailure)
                    return socialNetworkResult.Error.ToErrorList();

                socialNetworks.Add(socialNetworkResult.Value);
            }

            var userResult = User.CreatePartisipant(
                command.UserName,
                fullNameResult.Value,
                command.Email,
                role,
                socialNetworks);
            if (userResult.IsFailure)
                return userResult.Error.ToErrorList();

            var result = await _userManager.CreateAsync(userResult.Value, command.Password);
            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();

                return new ErrorList(errors);
            }

            var partisipantAccount = new PartisipantAccount()
            {
                Id = Guid.NewGuid(),
                UserId = userResult.Value.Id
            };

            await _accountsManager.CreatePartisipantAccount(partisipantAccount, cancellationToken);

            transaction.Commit();

            _logger.LogInformation("User created a new account partisipant");

            return Result.Success<ErrorList>(); 
        }
        catch(Exception ex)
        {
            _logger.LogError(ex,
                "Could not create partisipant user in transaction");

            transaction.Rollback();

            return Error.Failure("Could not create partisipant user in transaction", "user.register.failure").ToErrorList();
        }
    }
}
