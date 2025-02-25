using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using FluentValidation;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateSocialNetwork.Commands;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel;
using PetFamily.Core.Extentions;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Volunteers.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateSocialNetwork;

public class UpdateSocialNetworkHandler : ICommandHandler<Guid, UpdateSocialNetworkCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateSocialNetworkHandler> _logger;
    private readonly IValidator<UpdateSocialNetworkCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;


    public UpdateSocialNetworkHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateSocialNetworkHandler> logger,
        IValidator<UpdateSocialNetworkCommand> validator,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateSocialNetworkCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var id = VolunteerId.Create(command.Id);

        var volunteerResult = await _volunteerRepository.GetById(id);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var socialNetworks = new List<SocialNetwork>();

        if (command.SocialNetwork != null)
        {
            foreach (var socialnetwork in command.SocialNetwork)
            {
                var socialNetwork = SocialNetwork.Create(socialnetwork.Name, socialnetwork.Link).Value;

                socialNetworks.Add(socialNetwork);
            }
        }

        volunteerResult.Value.UpdateSocialNetwork(socialNetworks);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("updated social network volunteer {Surname} {Name} {Patronymic} with id {id}",
            volunteerResult.Value.FullName.Surname,
            volunteerResult.Value.FullName.Name,
            volunteerResult.Value.FullName.Patronymic,
            id.Value);

        return id.Value;
    }
}
