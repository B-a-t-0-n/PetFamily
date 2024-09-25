using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.Create.Commands;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Infrastucture.Repositories;

namespace PetFamily.Application.Volunteers.Create
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<CreateVolunteerHandler> _logger;

        public CreateVolunteerHandler(IVolunteerRepository volunteerRepository, ILogger<CreateVolunteerHandler> logger) 
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> Handle(CreateVolunteerCommand command, CancellationToken cancellationToken = default)
        {
            var volunteerId = VolunteerId.NewVolunteerId();

            var fullName = FullName.Create(command.FullName.Name, command.FullName.Surname, command.FullName.Patronymic).Value;

            var description = Description.Create(command.Description).Value;

            var yearsExperience = YearsExperience.Create(command.YearsExperience).Value;

            var phoneNumder = PhoneNumber.Create(command.PhoneNumber).Value;

            var detailsForAssistances = new List<DetailsForAssistance>();

            if (command.DetailsForAssistance != null)
            {
                foreach (var detailsForAssistance in command.DetailsForAssistance)
                {
                    var value = DetailsForAssistance.Create(detailsForAssistance.Name, detailsForAssistance.Description).Value;

                    detailsForAssistances.Add(value);
                }
            }

            var volunteerDetailsForAssistancek = new VolunteerDetailsForAssistance(detailsForAssistances);
            
            var socialNetworks = new List<SocialNetwork>();

            if(command.SocialNetworks != null)
            {
                foreach (var socialnetwork in command.SocialNetworks)
                {
                    var socialNetwork = SocialNetwork.Create(socialnetwork.Name, socialnetwork.Link).Value;

                    socialNetworks.Add(socialNetwork);
                }
            }

            var volunteerSocialNetworkResult = new VolunteerSocialNetwork(socialNetworks);

            var volunteerResult = Volunteer.Create(volunteerId,
                fullName,
                description,
                yearsExperience,
                phoneNumder,
                volunteerDetailsForAssistancek,
                volunteerSocialNetworkResult);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("created volunteer {Surname} {Name} {Patronymic} with id {volunteerId}", 
                fullName.Surname,
                fullName.Name,
                fullName.Patronymic,
                volunteerId.Value);

            return (Guid)volunteerResult.Value.Id;
        }
    }
}
