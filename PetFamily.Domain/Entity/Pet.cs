using CSharpFunctionalExtensions;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entity
{
    public class Pet : Shared.Entity<PetId>
    {
        private readonly List<PetPhoto> _petPhotos = [];

        //ef core
        private Pet(PetId id) : base(id) { }

        private Pet(PetId id, string nickname, string typeOfAnimals, string? description, string breedOfPet, string? color,
                    string? healthInformation, Address address, double weight, double height,
                    PhoneNumber phoneNumber, bool isCastrated, DateTime? dateOfBirth, bool isVaccinated,
                    AssistanceStatus assistanceStatus, DateTime dateOfCreation, PetDetailsForAssistance detailsForAssistance)
                    :base(id)
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
            DateOfCreation = dateOfCreation;
            DetailsForAssistance = detailsForAssistance;
        }

        public string Nickname { get; private set; } = default!;

        public string TypeOfAnimals { get; private set; } = default!;

        public string? Description { get; private set; }

        public string BreedOfPet { get; private set; } = default!;

        public string? Color { get; private set; }

        public string? HealthInformation { get; private set; }

        public Address Address { get; private set; } = default!;

        public double Weight { get; private set; } = default!;

        public double Height { get; private set; } = default!;

        public PhoneNumber PhoneNumber { get; private set; } = default!;

        public bool IsCastrated { get; private set; }

        public DateTime? DateOfBirth { get; private set; }

        public bool IsVaccinated { get; private set; }

        public AssistanceStatus AssistanceStatus { get; private set; } = default!;

        public DateTime DateOfCreation { get; private set; }

        public PetDetailsForAssistance DetailsForAssistance { get; private set; } = default!;

        public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;

        public void AddPet(PetPhoto petPhoto)
        {
            _petPhotos.Add(petPhoto);
        }

        public static Result<Pet> Create(PetId id, string nickname, string typeOfAnimals, string? description, string breedOfPet, string? color,
                                         string? healthInformation, Address address, double weight, double height,
                                         PhoneNumber phoneNumber, bool isCastrated, DateTime? dateOfBirth, bool isVaccinated,
                                         AssistanceStatus assistanceStatus, PetDetailsForAssistance detailsForAssistance)
        {
            if (string.IsNullOrWhiteSpace(nickname))
                Result.Failure<Pet>("nickname is null or white space");

            if (string.IsNullOrWhiteSpace(typeOfAnimals))
                Result.Failure<Pet>("typeOfAnimals is null or white space");

            if (string.IsNullOrWhiteSpace(breedOfPet))
                Result.Failure<Pet>("breedOfPet is null or white space");

            if (weight < 0)
                Result.Failure<Pet>("weight < 0");

            if (height < 0)
                Result.Failure<Pet>("height < 0");

            var dateOfCreation = DateTime.Now;

            var pet = new Pet(id, nickname, typeOfAnimals, description, breedOfPet, color, healthInformation, address, weight, height, phoneNumber, isCastrated,
                              dateOfBirth, isVaccinated, assistanceStatus, dateOfCreation, detailsForAssistance);

            return Result.Success(pet);
        }
    }
}
