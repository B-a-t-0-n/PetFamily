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
            ValueObjectList<DetailsForAssistance>? detailsForAssistance,
            ValueObjectList<SocialNetwork>? socialNetwork
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

        public ValueObjectList<SocialNetwork>? SocialNetwork { get; private set; }

        public ValueObjectList<DetailsForAssistance>? DetailsForAssistance { get; private set; } = default!;

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
            ValueObjectList<DetailsForAssistance>? detailsForAssistance,
            ValueObjectList<SocialNetwork>? socialNetwork)
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

        public void UpdateSocialNetwork(ValueObjectList<SocialNetwork>? socialNetwork)
        {
            SocialNetwork = socialNetwork;
        }

        public void UpdateDetailsForAssistance(ValueObjectList<DetailsForAssistance>? detailsForAssistance)
        {
            DetailsForAssistance = detailsForAssistance;
        }

        public UnitResult<Error> AddPet(Pet pet)
        {
            var serialNumberResult = SerialNumber.Create(_pets.Count + 1);
            if (serialNumberResult.IsFailure)
            {
                return serialNumberResult.Error;
            }

            pet.SetSerialNumber(serialNumberResult.Value);

            _pets.Add(pet);
            return Result.Success<Error>();
        }

        public UnitResult<Error> MovePet(Pet pet, SerialNumber newSerialNumber)
        {
            var currentSerialNumber = pet.SerialNumber;

            if (currentSerialNumber == newSerialNumber || _pets.Count == 1)
                return Result.Success<Error>();

            var adjustedSerialNumber = AdjustNewSerialNumberIfOutOfRange(newSerialNumber);
            if (adjustedSerialNumber.IsFailure)
                return adjustedSerialNumber.Error;

            newSerialNumber = adjustedSerialNumber.Value;

            var moveResult = MovePetBetweenSerialNumbers(newSerialNumber, currentSerialNumber);
            if (moveResult.IsFailure)
                return moveResult.Error;

            pet.Move(newSerialNumber);

            return Result.Success<Error>();
        }

        private Result<SerialNumber, Error> AdjustNewSerialNumberIfOutOfRange(SerialNumber newSerialNumber)
        {
            if (newSerialNumber.Value <= _pets.Count)
                return newSerialNumber;

            var lasrSerialNumber = SerialNumber.Create(_pets.Count);
            if(lasrSerialNumber.IsFailure)
                return lasrSerialNumber.Error;

            return lasrSerialNumber.Value;
        }

        private UnitResult<Error> MovePetBetweenSerialNumbers(SerialNumber newSerialNumber, SerialNumber currentSerialNumber)
        {
            if(newSerialNumber.Value < currentSerialNumber.Value)
            {
                var petsToMove = _pets
                    .Where(p => p.SerialNumber.Value >= newSerialNumber.Value && p.SerialNumber.Value < currentSerialNumber.Value);

                foreach(var petToMove in petsToMove)
                {
                    var result = petToMove.MoveForward();
                    if (result.IsFailure)
                        return result.Error;
                }
 
            }
            else if(newSerialNumber.Value > currentSerialNumber.Value)
            {
                var petsToMove = _pets
                    .Where(p => p.SerialNumber.Value > currentSerialNumber.Value && p.SerialNumber.Value <= newSerialNumber.Value);

                foreach (var petToMove in petsToMove)
                {
                    var result = petToMove.MoveBack();
                    if (result.IsFailure)
                        return result.Error;
                }
            }

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
