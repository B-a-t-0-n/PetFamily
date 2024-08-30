using CSharpFunctionalExtensions;
using PetFamily.Application.Volunteers.CreateVolunteer.Requests;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.PetMenegment.ValueObjects;
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

        public async Task<Result<Guid, string>> Handle(CreateVolunteerRequest request, CancellationToken cancellationToken = default)
        {
            var volunteerId = VolunteerId.NewVolunteerId();

            var fullNameResult = FullName.Create(request.FullName.Name, request.FullName.Surname, request.FullName.Patronymic);
            if (fullNameResult.IsFailure)
                return fullNameResult.Error;

            var descriptionResult = Description.Create(request.Description);
            if (descriptionResult.IsFailure)
                return descriptionResult.Error;

            var yearsExperienceResult = YearsExperience.Create(request.YearsExperience);
            if (yearsExperienceResult.IsFailure)
                return yearsExperienceResult.Error;

            var numberPetsResult = NumberPets.Create(request.NumberPets.FoundAHouse, request.NumberPets.LookingForHouse, request.NumberPets.BeingTreated);
            if (numberPetsResult.IsFailure)
                return numberPetsResult.Error;

            var phoneNumderResult = PhoneNumber.Create(request.PhoneNumber);
            if (phoneNumderResult.IsFailure)
                return phoneNumderResult.Error;

            var detailsForAssistances = new List<DetailsForAssistance>();

            if (request.DetailsForAssistance != null)
            {
                foreach (var detailsForAssistance in request.DetailsForAssistance)
                {
                    var socialNetworkResult = DetailsForAssistance.Create(detailsForAssistance.Name, detailsForAssistance.Description);
                    if (socialNetworkResult.IsFailure)
                        return socialNetworkResult.Error;

                    detailsForAssistances.Add(socialNetworkResult.Value);
                }
            }

            var volunteerDetailsForAssistancekResult = VolunteerDetailsForAssistance.Create(detailsForAssistances);
            if (volunteerDetailsForAssistancekResult.IsFailure)
                return volunteerDetailsForAssistancekResult.Error;
            
            var socialNetworks = new List<SocialNetwork>();

            if(request.SocialNetworks != null)
            {
                foreach (var socialnetwork in request.SocialNetworks)
                {
                    var socialNetworkResult = SocialNetwork.Create(socialnetwork.Name, socialnetwork.Link);
                    if (socialNetworkResult.IsFailure)
                        return socialNetworkResult.Error;

                    socialNetworks.Add(socialNetworkResult.Value);
                }
            }

            var volunteerSocialNetworkResult = VolunteerSocialNetwork.Create(socialNetworks);
            if (volunteerSocialNetworkResult.IsFailure)
                return volunteerSocialNetworkResult.Error;

            var volunteerResult = Volunteer.Create(volunteerId,
                fullNameResult.Value,
                descriptionResult.Value,
                yearsExperienceResult.Value,
                numberPetsResult.Value,
                phoneNumderResult.Value,
                volunteerDetailsForAssistancekResult.Value,
                volunteerSocialNetworkResult.Value);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);

            return (Guid)volunteerResult.Value.Id;
        }
    }
}
