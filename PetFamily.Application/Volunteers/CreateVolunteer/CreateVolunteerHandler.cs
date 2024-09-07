using CSharpFunctionalExtensions;
using PetFamily.Application.Volunteers.CreateVolunteer.Requests;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Infrastucture.Repositories;

namespace PetFamily.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;

        public CreateVolunteerHandler(IVolunteerRepository volunteerRepository) 
        {
            _volunteerRepository = volunteerRepository;
        }

        public async Task<Result<Guid, Error>> Handle(CreateVolunteerRequest request, CancellationToken cancellationToken = default)
        {
            var volunteerId = VolunteerId.NewVolunteerId();

            var fullNameResult = FullName.Create(request.FullName.Name, request.FullName.Surname, request.FullName.Patronymic).Value;

            var descriptionResult = Description.Create(request.Description).Value;

            var yearsExperienceResult = YearsExperience.Create(request.YearsExperience).Value;

            var phoneNumderResult = PhoneNumber.Create(request.PhoneNumber).Value;

            var detailsForAssistances = new List<DetailsForAssistance>();

            if (request.DetailsForAssistance != null)
            {
                foreach (var detailsForAssistance in request.DetailsForAssistance)
                {
                    var socialNetworkResult = DetailsForAssistance.Create(detailsForAssistance.Name, detailsForAssistance.Description).Value;

                    detailsForAssistances.Add(socialNetworkResult);
                }
            }

            var volunteerDetailsForAssistancekResult = new VolunteerDetailsForAssistance(detailsForAssistances);
            
            var socialNetworks = new List<SocialNetwork>();

            if(request.SocialNetworks != null)
            {
                foreach (var socialnetwork in request.SocialNetworks)
                {
                    var socialNetworkResult = SocialNetwork.Create(socialnetwork.Name, socialnetwork.Link).Value;

                    socialNetworks.Add(socialNetworkResult);
                }
            }

            var volunteerSocialNetworkResult = new VolunteerSocialNetwork(socialNetworks);

            var volunteerResult = Volunteer.Create(volunteerId,
                fullNameResult,
                descriptionResult,
                yearsExperienceResult,
                phoneNumderResult,
                volunteerDetailsForAssistancekResult,
                volunteerSocialNetworkResult);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);

            return (Guid)volunteerResult.Value.Id;
        }
    }
}
