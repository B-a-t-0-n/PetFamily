using CSharpFunctionalExtensions;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain
{
    public class Pet
    {
        //ef core
        private Pet()
        {

        }

        private Pet(string nickname, string typeOfAnimals, string? description, string breedOfPet, string? color,
                    string? healthInformation, Address address, PhysicalQuantity weight, PhysicalQuantity height,
                    PhoneNumber? phoneNumber, bool isCastrated, DateTime? dateOfBirth, bool isVaccinated,
                    AssistanceStatus assistanceStatus, DetailsForAssistance detailsForAssistance, DateTime dateOfCreation)
        {
            Nickname = nickname;
            TypeOfAnimals = typeOfAnimals;
            Description = description;
            BreedOfPet = breedOfPet;
            Color = color;
            HealthInformation = healthInformation;
            Address = address;
            Weight = weight;
            Height = height;
            PhoneNumber = phoneNumber;
            IsCastrated = isCastrated;
            DateOfBirth = dateOfBirth;
            IsVaccinated = isVaccinated;
            AssistanceStatus = assistanceStatus;
            DetailsForAssistance = detailsForAssistance;
            DateOfCreation = dateOfCreation;
        }

        public Guid Id { get; protected set; }

        public string Nickname { get; protected set; } = default!;

        public string TypeOfAnimals { get; protected set; } = default!;

        public string? Description { get; protected set; }

        public string BreedOfPet { get; protected set; } = default!;

        public string? Color { get; protected set; }

        public string? HealthInformation { get; protected set; }

        public Address Address { get; protected set; } = default!;

        public PhysicalQuantity Weight { get; protected set; } = default!;

        public PhysicalQuantity Height { get; protected set; } = default!;

        public PhoneNumber? PhoneNumber { get; protected set; } 

        public bool IsCastrated { get; protected set; }

        public DateTime? DateOfBirth { get; protected set; }

        public bool IsVaccinated { get; protected set; }

        public AssistanceStatus AssistanceStatus { get; protected set; } = default!;

        public DetailsForAssistance DetailsForAssistance { get; protected set; } = default!;

        public DateTime DateOfCreation { get; protected set; }

        public static Result<Pet> Create(string nickname, string typeOfAnimals, string? description, string breedOfPet, string? color,
                                         string? healthInformation, Address address, PhysicalQuantity weight, PhysicalQuantity height,
                                         PhoneNumber? phoneNumber, bool isCastrated, DateTime? dateOfBirth, bool isVaccinated,
                                         AssistanceStatus assistanceStatus, DetailsForAssistance detailsForAssistance)
        {
            if (string.IsNullOrWhiteSpace(nickname))
                Result.Failure<Pet>("nickname is null or white space");

            if (string.IsNullOrWhiteSpace(typeOfAnimals))
                Result.Failure<Pet>("typeOfAnimals is null or white space");

            if (string.IsNullOrWhiteSpace(breedOfPet))
                Result.Failure<Pet>("breedOfPet is null or white space");

            var dateOfCreation = DateTime.Now;

            var pet = new Pet(nickname, typeOfAnimals, description, breedOfPet, color, healthInformation, address, weight, height, phoneNumber, isCastrated,
                              dateOfBirth, isVaccinated, assistanceStatus, detailsForAssistance, dateOfCreation);

            return Result.Success(pet);
        }
    }
}
