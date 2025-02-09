using CSharpFunctionalExtensions;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Domain.PetMenegment.Entity
{
    public class Volunteer : Shared.SoftDeletableEntity<VolunteerId>
    {         
        private readonly List<Pet> _pets = [];
        public List<SocialNetwork> _socialNetwork = [];
        public List<DetailsForAssistance> _detailsForAssistance = [];

        //ef core
        private Volunteer(VolunteerId id) : base(id) { }

        private Volunteer(
            VolunteerId id,
            FullName fullName,
            Description description,
            YearsExperience yearsExperience,
            PhoneNumber phoneNumber,
            List<DetailsForAssistance> detailsForAssistance,
            List<SocialNetwork> socialNetwork
            ) : base(id)
        {
            FullName = fullName;
            Description = description;
            YearsExperience = yearsExperience;
            PhoneNumber = phoneNumber;
            _detailsForAssistance = detailsForAssistance;
            _socialNetwork = socialNetwork;
        }

        public FullName FullName { get; private set; } = default!;

        public Description Description { get; private set; } = default!;

        public YearsExperience YearsExperience { get; private set; } = default!;

        public PhoneNumber PhoneNumber { get; private set; } = default!;

        public IReadOnlyList<SocialNetwork> SocialNetwork => _socialNetwork;

        public IReadOnlyList<DetailsForAssistance> DetailsForAssistance => _detailsForAssistance;

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
            List<DetailsForAssistance> detailsForAssistance,
            List<SocialNetwork> socialNetwork)
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

        public void UpdateSocialNetwork(List<SocialNetwork> socialNetwork)
        {
            _socialNetwork = socialNetwork;
        }

        public void UpdateDetailsForAssistance(List<DetailsForAssistance> detailsForAssistance)
        {
            _detailsForAssistance = detailsForAssistance;
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

        public UnitResult<Error> UpdatePetInfo(
            PetId petId,
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
            List<DetailsForAssistance> detailsForAssistance
            )
        {
            var pet = _pets.FirstOrDefault(p => p.Id == petId);
            if (pet is null)
                return Errors.General.NotFound(petId);

            pet.UpdateInfo(nickname,
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
                detailsForAssistance);

            return Result.Success<Error>();
        }

        public UnitResult<Error> UpdatePetAssistanceStatus(PetId petId, AssistanceStatus assistanceStatus)
        {
            var pet = _pets.FirstOrDefault(p => p.Id == petId);
            if (pet is null)
                return Errors.General.NotFound(petId);

            pet.UpdateAssistanceStatus(assistanceStatus);

            return Result.Success<Error>();
        }

        public UnitResult<Error> AddPetPhoto(PetId petId, PetPhoto photo)
        {
            var pet = _pets.FirstOrDefault(p => p.Id == petId);
            if (pet is null)
                return Errors.General.NotFound(petId);

            pet.AddPhoto(photo);

            return Result.Success<Error>();
        }

        public UnitResult<Error> SetMainPhotoPet(PetId petId, PhotoPath petPhoto)
        {
            var pet = _pets.FirstOrDefault(p => p.Id == petId);
            if (pet is null)
                return Errors.General.NotFound(petId);

            var result = pet.SetMainPhoto(petPhoto);
            if (result.IsFailure)
                return result.Error;

            return Result.Success<Error>();
        }

        public Result<string, Error> DeletePetPhoto(PetId petId, PetPhotoId photoId)
        {
            var pet = _pets.FirstOrDefault(p => p.Id == petId);
            if (pet is null)
                return Errors.General.NotFound(petId);

            var photo = pet.PetPhotos.FirstOrDefault(p => p.Id == photoId);
            if (photo is null)
                return Errors.General.NotFound(photoId);

            pet.DeletePhoto(photo);

            return photo.Path.PathToStorage;
        }

        public UnitResult<Error> SoftDeletePet(PetId id)
        {
            var pet = _pets.FirstOrDefault(p => p.Id == id);
            if (pet is null)
                return Errors.General.NotFound(id);

            pet.Delete();

            return Result.Success<Error>();
        }

        public UnitResult<Error> HardDeletePet(PetId id)
        {
            var pet = _pets.FirstOrDefault(p => p.Id == id);
            if (pet is null)
                return Errors.General.NotFound(id);

            _pets.Remove(pet);

            return Result.Success<Error>();
        }

        public override void Delete()
        {
            base.Delete();

            foreach (var pet in _pets)
            {
                pet.Delete();
            }
        }

        public override void Restore()
        {
            base.Restore();

            foreach (var pet in _pets)
            {
                pet.Restore();
            }
        }

        public void DeleteExpiredPets()
        {
            _pets.RemoveAll(p => p.IsExpired());
        }

        public bool IsExpired() => DeletionDate != null
                                   && DateTime.UtcNow >= DeletionDate.Value
                                      .AddDays(Constants.LIFETIME_AFTER_DELETION);

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

        
    }
}
