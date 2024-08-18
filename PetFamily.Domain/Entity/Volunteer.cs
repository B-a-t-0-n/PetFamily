using CSharpFunctionalExtensions;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entity
{
    public class Volunteer
    {
        private readonly List<Pet> _pets = [];

        //ef core
        private Volunteer()
        {

        }

        private Volunteer(FullName fullName, string? description, int yearsExperience, NumberPets numberPets, PhoneNumber phoneNumber,
                          DetailsForAssistance detailsForAssistance, VolunteerSocialNetwork? socialNetwork)
        {
            FullName = fullName;
            Description = description;
            YearsExperience = yearsExperience;
            NumberPets = numberPets;
            PhoneNumber = phoneNumber;
            DetailsForAssistance = detailsForAssistance;
            SocialNetwork = socialNetwork;
        }

        public Guid Id { get; private set; }

        public FullName FullName { get; private set; } = default!;

        public string? Description { get; private set; }

        public int YearsExperience { get; private set; }

        public NumberPets NumberPets { get; private set; } = default!;

        public PhoneNumber PhoneNumber { get; private set; } = default!;

        public VolunteerSocialNetwork? SocialNetwork { get; private set; }

        public DetailsForAssistance DetailsForAssistance { get; private set; } = default!;

        public IReadOnlyList<Pet> Pets => _pets;

        public void AddPet(Pet pet)
        {
            _pets.Add(pet);
        }

        public static Result<Volunteer> Create(FullName fullName, string? description, int yearsExperience, NumberPets numberPets, PhoneNumber phoneNumber,
                                               DetailsForAssistance detailsForAssistance, VolunteerSocialNetwork? socialNetwork)
        {
            if (yearsExperience < 0)
                Result.Failure<Volunteer>("yearsExperience < 0");

            var volunteer = new Volunteer(fullName!, description, yearsExperience, numberPets!, phoneNumber!, detailsForAssistance, socialNetwork);

            return Result.Success(volunteer);
        }

    }
}
