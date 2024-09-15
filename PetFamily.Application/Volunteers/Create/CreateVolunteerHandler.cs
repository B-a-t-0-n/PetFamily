using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.Create.Requests;
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

        public async Task<Result<Guid, Error>> Handle(CreateVolunteerRequest request, CancellationToken cancellationToken = default)
        {
            var volunteerId = VolunteerId.NewVolunteerId();

            var fullName = FullName.Create(request.FullName.Name, request.FullName.Surname, request.FullName.Patronymic).Value;

            var description = Description.Create(request.Description).Value;

            var yearsExperience = YearsExperience.Create(request.YearsExperience).Value;

            var phoneNumder = PhoneNumber.Create(request.PhoneNumber).Value;

            var detailsForAssistances = new List<DetailsForAssistance>();

            if (request.DetailsForAssistance != null)
            {
                foreach (var detailsForAssistance in request.DetailsForAssistance)
                {
                    var socialNetwork = DetailsForAssistance.Create(detailsForAssistance.Name, detailsForAssistance.Description).Value;

                    detailsForAssistances.Add(socialNetwork);
                }
            }

            var volunteerDetailsForAssistancek = new VolunteerDetailsForAssistance(detailsForAssistances);
            
            var socialNetworks = new List<SocialNetwork>();

            if(request.SocialNetworks != null)
            {
                foreach (var socialnetwork in request.SocialNetworks)
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

            _logger.LogInformation("created volunteer {fullNameResult} with id {volunteerId}", 
                $"{fullName.Surname} {fullName.Name} {fullName.Patronymic}",
                volunteerId.Value);

            return (Guid)volunteerResult.Value.Id;
        }
    }
}
