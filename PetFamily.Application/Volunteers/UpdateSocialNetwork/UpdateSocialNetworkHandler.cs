using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Infrastucture.Repositories;
using PetFamily.Domain.Shared;
using PetFamily.Application.Volunteers.UpdateSocialNetwork.Commands;
using PetFamily.Application.Database;
using FluentValidation;
using PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Commands;
using PetFamily.Application.Extentions;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork
{
    public class UpdateSocialNetworkHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateSocialNetworkHandler> _logger;
        private readonly IValidator<UpdateSocialNetworkCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateSocialNetworkHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<UpdateSocialNetworkHandler> logger,
            IValidator<UpdateSocialNetworkCommand> validator,
            IUnitOfWork unitOfWork)
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
}
