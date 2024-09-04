using CSharpFunctionalExtensions;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Domain.PetMenegment.Entity
{
    public class Volunteer : Shared.Entity<VolunteerId>
    {
        private readonly List<Pet> _pets = [];

        //ef core
        private Volunteer(VolunteerId id) : base(id) { }

        private Volunteer(VolunteerId id,
            FullName fullName,
            Description description,
            YearsExperience yearsExperience,
            NumberPets numberPets,
            PhoneNumber phoneNumber,
            VolunteerDetailsForAssistance? detailsForAssistance,
            VolunteerSocialNetwork? socialNetwork
            ) : base(id)
        {
            FullName = fullName;
            Description = description;
            YearsExperience = yearsExperience;
            NumberPets = numberPets;
            PhoneNumber = phoneNumber;
            DetailsForAssistance = detailsForAssistance;
            SocialNetwork = socialNetwork;
        }

        public FullName FullName { get; private set; } = default!;

        public Description Description { get; private set; } = default!;

        public YearsExperience YearsExperience { get; private set; } = default!;

        public NumberPets NumberPets { get; private set; } = default!;

        public PhoneNumber PhoneNumber { get; private set; } = default!;

        public VolunteerSocialNetwork? SocialNetwork { get; private set; }

        public VolunteerDetailsForAssistance? DetailsForAssistance { get; private set; } = default!;

        public IReadOnlyList<Pet> Pets => _pets;

        public void AddPet(Pet pet)
        {
            _pets.Add(pet);
        }

        public static Result<Volunteer> Create(VolunteerId id,
            FullName fullName,
            Description description,
            YearsExperience yearsExperience,
            NumberPets numberPets,
            PhoneNumber phoneNumber,
            VolunteerDetailsForAssistance? detailsForAssistance,
            VolunteerSocialNetwork? socialNetwork)
        {
            var volunteer = new Volunteer(id, fullName!, description, yearsExperience, numberPets!, phoneNumber!, detailsForAssistance, socialNetwork);

            return Result.Success(volunteer);
        }

    }
}
