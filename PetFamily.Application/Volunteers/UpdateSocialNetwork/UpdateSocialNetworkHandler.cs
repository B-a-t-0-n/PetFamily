using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Infrastucture.Repositories;
using PetFamily.Domain.Shared;
using PetFamily.Application.Volunteers.UpdateSocialNetwork.Commands;
using PetFamily.Application.Database;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork
{
    public class UpdateSocialNetworkHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateSocialNetworkHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateSocialNetworkHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<UpdateSocialNetworkHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, Error>> Handle(UpdateSocialNetworkCommand command, CancellationToken cancellationToken = default)
        {
            var id = VolunteerId.Create(command.Id);

            var volunteerResult = await _volunteerRepository.GetById(id);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var socialNetworks = new List<SocialNetwork>();

            if (command.SocialNetworkDto.SocialNetwork != null)
            {
                foreach (var socialnetwork in command.SocialNetworkDto.SocialNetwork)
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
