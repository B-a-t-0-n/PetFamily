using CSharpFunctionalExtensions;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Domain.PetMenegment.Entity
{
    public class Pet : Shared.Entity<PetId>
    {
        private readonly List<PetPhoto> _petPhotos = [];

        //ef core
        private Pet(PetId id) : base(id) { }

        private Pet(PetId id,
            Nickname nickname,
            string typeOfAnimals,
            Description description,
            string breedOfPet,
            Color color,
            HealthInformation healthInformation,
            Address address,
            Size size,
            PhoneNumber phoneNumber,
            bool isCastrated,
            DateTime? dateOfBirth,
            bool isVaccinated,
            AssistanceStatus assistanceStatus,
            DateTime dateOfCreation,
            PetDetailsForAssistance detailsForAssistance
            ) : base(id)
        {
            Nickname = nickname;
            TypeOfAnimals = typeOfAnimals;
            Description = description;
            BreedOfPet = breedOfPet;
            Color = color;
            HealthInformation = healthInformation;
            Address = address;
            Size = size;
            PhoneNumber = phoneNumber;
            IsCastrated = isCastrated;
            DateOfBirth = dateOfBirth;
            IsVaccinated = isVaccinated;
            AssistanceStatus = assistanceStatus;
            DateOfCreation = dateOfCreation;
            DetailsForAssistance = detailsForAssistance;
        }

        public Nickname Nickname { get; private set; } = default!;

        public string TypeOfAnimals { get; private set; } = default!;//

        public Description Description { get; private set; }

        public string BreedOfPet { get; private set; } = default!;//

        public Color Color { get; private set; }

        public HealthInformation HealthInformation { get; private set; }

        public Address Address { get; private set; } = default!;

        public Size Size { get; private set; } = default!;

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

        public static Result<Pet> Create(PetId id,
            Nickname nickname,
            string typeOfAnimals,
            Description description,
            string breedOfPet,
            Color color,
            HealthInformation healthInformation,
            Address address,
            Size size,
            PhoneNumber phoneNumber,
            bool isCastrated,
            DateTime? dateOfBirth,
            bool isVaccinated,
            AssistanceStatus assistanceStatus,
            DateTime dateOfCreation,
            PetDetailsForAssistance detailsForAssistance)
        {
            if (string.IsNullOrWhiteSpace(typeOfAnimals))
                Result.Failure<Pet>("typeOfAnimals is null or white space");

            if (string.IsNullOrWhiteSpace(breedOfPet))
                Result.Failure<Pet>("breedOfPet is null or white space");

            var pet = new Pet(id, nickname, typeOfAnimals, description, breedOfPet, color, healthInformation, address, size, phoneNumber, isCastrated,
                              dateOfBirth, isVaccinated, assistanceStatus, dateOfCreation, detailsForAssistance);

            return Result.Success(pet);
        }
    }
}
