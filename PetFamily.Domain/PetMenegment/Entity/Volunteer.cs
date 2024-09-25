using CSharpFunctionalExtensions;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Domain.PetMenegment.Entity
{
    public class Volunteer : Shared.Entity<VolunteerId>, ISoftDeletable
    {
        private bool _isDeleted = false;
         
        private readonly List<Pet> _pets = [];

        //ef core
        private Volunteer(VolunteerId id) : base(id) { }

        private Volunteer(
            VolunteerId id,
            FullName fullName,
            Description description,
            YearsExperience yearsExperience,
            PhoneNumber phoneNumber,
            VolunteerDetailsForAssistance? detailsForAssistance,
            VolunteerSocialNetwork? socialNetwork
            ) : base(id)
        {
            FullName = fullName;
            Description = description;
            YearsExperience = yearsExperience;
            PhoneNumber = phoneNumber;
            DetailsForAssistance = detailsForAssistance;
            SocialNetwork = socialNetwork;
        }

        public FullName FullName { get; private set; } = default!;

        public Description Description { get; private set; } = default!;

        public YearsExperience YearsExperience { get; private set; } = default!;

        public PhoneNumber PhoneNumber { get; private set; } = default!;

        public VolunteerSocialNetwork? SocialNetwork { get; private set; }

        public VolunteerDetailsForAssistance? DetailsForAssistance { get; private set; } = default!;

        public IReadOnlyList<Pet> Pets => _pets;

        public int FoundAHousePets =>
            _pets
            .Count(p => p.AssistanceStatus == AssistanceStatus.FoundAHouse);

        public int LookingForHomePets =>
            _pets
            .Count(p => p.AssistanceStatus == AssistanceStatus.LookingForHome);

        public int NeedsHelpPets =>
            _pets
            .Count(p => p.AssistanceStatus == AssistanceStatus.NeedsHelp);

        public static Result<Volunteer, Error> Create(
            VolunteerId id,
            FullName fullName,
            Description description,
            YearsExperience yearsExperience,
            PhoneNumber phoneNumber,
            VolunteerDetailsForAssistance? detailsForAssistance,
            VolunteerSocialNetwork? socialNetwork)
        {
            var volunteer = new Volunteer(id, fullName!, description, yearsExperience, phoneNumber!, detailsForAssistance, socialNetwork);

            return volunteer;
        }

        public void UpdateMainInfo(
            FullName fullName,
            Description description,
            YearsExperience yearsExperience,
            PhoneNumber phoneNumber)
        {
            FullName = fullName;
            Description = description;
            YearsExperience = yearsExperience;
            PhoneNumber = phoneNumber;
        }

        public void UpdateSocialNetwork(VolunteerSocialNetwork socialNetwork)
        {
            SocialNetwork = socialNetwork;
        }

        public void UpdateDetailsForAssistance(VolunteerDetailsForAssistance detailsForAssistance)
        {
            DetailsForAssistance = detailsForAssistance;
        }

        public UnitResult<Error> AddPet(Pet pet)
        {
            _pets.Add(pet);
            return Result.Success<Error>();
        }

        public void Delete()
        {
            if(_isDeleted == false)
            {
                _isDeleted = true;
            }
        }

        public void Restore()
        {
            if (_isDeleted)
            {
                _isDeleted = true;
            }
        }
    }
}
