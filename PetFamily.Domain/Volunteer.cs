using CSharpFunctionalExtensions;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain
{
    public class Volunteer
    {
        private readonly List<SocialNetwork> _socialNetwork = [];
        private readonly List<Pet> _pets = [];

        //ef core
        private Volunteer() 
        {
            
        }

        private Volunteer(FullName fullName, string? description, int yearsExperience, NumberPets numberPets, PhoneNumber phoneNumber,
                          DetailsForAssistance? detailsForAssistance)
        {
            FullName = fullName;
            Description = description;
            YearsExperience = yearsExperience;
            NumberPets = numberPets;
            PhoneNumber = phoneNumber;
            DetailsForAssistance = detailsForAssistance;
        }

        public Guid Id { get; private set; }

        public FullName FullName { get; private set; } = default!;

        public string? Description { get; private set; }

        public int YearsExperience { get; private set; }

        public NumberPets NumberPets { get; private set; } = default!;

        public PhoneNumber PhoneNumber { get; private set; } = default!;

        public IReadOnlyList<SocialNetwork> SocialNetwork => _socialNetwork;

        public DetailsForAssistance? DetailsForAssistance { get; private set; }

        public IReadOnlyList<Pet> Pets => _pets;

        public void AddSocialNetwork(SocialNetwork socialNetwork)
        {
            _socialNetwork.Add(socialNetwork);
        }

        public void AddPet(Pet pet)
        {
            _pets.Add(pet);
        }

        public static Result<Volunteer> Create(FullName fullName, string? description, int yearsExperience, NumberPets numberPets, PhoneNumber phoneNumber,
                                               DetailsForAssistance? detailsForAssistance)
        {
            if (yearsExperience < 0)
                Result.Failure<Volunteer>("yearsExperience < 0");

            if (fullName == null)
                Result.Failure<Volunteer>("fullName = null");

            if (numberPets == null)
                Result.Failure<Volunteer>("numberPets = null");

            if (phoneNumber == null)
                Result.Failure<Volunteer>("phoneNumber = null");

            var volunteer = new Volunteer(fullName!, description, yearsExperience, numberPets!, phoneNumber!, detailsForAssistance);

            return Result.Success(volunteer);
        }

    }
}
