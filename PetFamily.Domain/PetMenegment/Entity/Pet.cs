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
            SpeciesAndBreed speciesAndBreed,
            Description description,
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
            Description = description;
            Color = color;
            HealthInformation = healthInformation;
            SpeciesAndBreed = speciesAndBreed;
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

        public SpeciesAndBreed SpeciesAndBreed { get; private set; } = default!;

        public Description Description { get; private set; } = default!;

        public Color Color { get; private set; } = default!;

        public HealthInformation HealthInformation { get; private set; } = default!;

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
            SpeciesAndBreed speciesAndBreed,
            Description description,
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
            var pet = new Pet(id, nickname, speciesAndBreed, description, color, healthInformation, address, size, phoneNumber, isCastrated,
                              dateOfBirth, isVaccinated, assistanceStatus, dateOfCreation, detailsForAssistance);

            return Result.Success(pet);
        }
    }
}
