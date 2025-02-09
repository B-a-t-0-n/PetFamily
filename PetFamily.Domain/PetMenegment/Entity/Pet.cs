using CSharpFunctionalExtensions;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;
using System.Drawing;
using System.Net;
using Color = PetFamily.Domain.PetMenegment.ValueObjects.Color;
using Size = PetFamily.Domain.PetMenegment.ValueObjects.Size;

namespace PetFamily.Domain.PetMenegment.Entity
{
    public class Pet : Shared.Entity<PetId>, ISoftDeletable
    {
        private bool _isDeleted = false;

        private readonly List<PetPhoto> _petPhotos = [];
        private List<DetailsForAssistance> _detailsForAssistance = [];


        //ef core
        private Pet(PetId id) : base(id) { }

        private Pet(
            PetId id,
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
            List<DetailsForAssistance> detailsForAssistance
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
            _detailsForAssistance = detailsForAssistance;
        }

        public Nickname Nickname { get; private set; } = default!;

        public SpeciesAndBreed SpeciesAndBreed { get; private set; } = default!;

        public Description Description { get; private set; } = default!;

        public Color Color { get; private set; } = default!;

        public SerialNumber SerialNumber { get; private set; } = default!;

        public HealthInformation HealthInformation { get; private set; } = default!;

        public Address Address { get; private set; } = default!;

        public Size Size { get; private set; } = default!;

        public PhoneNumber PhoneNumber { get; private set; } = default!;

        public bool IsCastrated { get; private set; }

        public DateTime? DateOfBirth { get; private set; }

        public bool IsVaccinated { get; private set; }

        public AssistanceStatus AssistanceStatus { get; private set; } = default!;

        public DateTime DateOfCreation { get; private set; }

        public IReadOnlyList<DetailsForAssistance> DetailsForAssistance => _detailsForAssistance;

        public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;

        public void AddPetPhoto(PetPhoto petPhoto)
        {
            _petPhotos.Add(petPhoto);
        }

        public void DeletePetPhoto(PetPhoto petPhoto)
        {
            _petPhotos.Remove(petPhoto);
        }

        public static Result<Pet, Error> Create(
            PetId id,
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
            List<DetailsForAssistance> detailsForAssistance)
        {
            var pet = new Pet(id,
                nickname,
                speciesAndBreed,
                description,
                color,
                healthInformation,
                address,
                size,
                phoneNumber,
                isCastrated,
                dateOfBirth,
                isVaccinated,
                assistanceStatus,
                dateOfCreation,
                detailsForAssistance);

            return pet;
        }

        public void UpdateInfo(
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
            List<DetailsForAssistance> detailsForAssistance)
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
            _detailsForAssistance = detailsForAssistance;
        }

        public void Delete()
        {
            if (_isDeleted == false)
            {
                _isDeleted = true;
            }
        }

        public void Restore()
        {
            if (_isDeleted)
            {
                _isDeleted = false;
            }
        }

        public void SetSerialNumber(SerialNumber serialNumber) => SerialNumber = serialNumber;

        public UnitResult<Error> MoveForward()
        {
            var newSerialNumber = SerialNumber.Forward();
            if (newSerialNumber.IsFailure)
                return newSerialNumber.Error;

            SerialNumber = newSerialNumber.Value;

            return Result.Success<Error>();
        }

        public UnitResult<Error> MoveBack()
        {
            var newSerialNumber = SerialNumber.Back();
            if (newSerialNumber.IsFailure)
                return newSerialNumber.Error;

            SerialNumber = newSerialNumber.Value;

            return Result.Success<Error>();
        }

        public void Move(SerialNumber newSerialNumber) => SerialNumber = newSerialNumber;
    }
}
