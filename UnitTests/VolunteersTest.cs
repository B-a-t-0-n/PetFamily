using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;

namespace UnitTests
{
    public class VolunteersTest
    {

        [Fact]
        public void AddPet_ShouldAssignSerialNumber_WhenFirstPetIsAdded()
        {
            // Arrange
            var volunteer = Volunteer.Create(
                VolunteerId.Create(Guid.NewGuid()),
                FullName.Create("John", "Doe", null).Value,
                Description.Create("Volunteer description").Value,
                YearsExperience.Create(5).Value,
                PhoneNumber.Create("1234567890").Value,
                null,
                null
            ).Value;

            var pet = Pet.Create(
                PetId.NewPetId(),
                Nickname.Create("Buddy").Value,
                SpeciesAndBreed.Create(SpeciesId.Create(Guid.NewGuid()), Guid.NewGuid()).Value,
                Description.Create("Pet description").Value,
                Color.Create("Brown").Value,
                HealthInformation.Create("Healthy").Value,
                Address.Create("City", "Street", "House", null, null).Value,
                Size.Create(50, 20).Value,
                PhoneNumber.Create("0987654321").Value,
                true,
                DateTime.Now,
                true,
                AssistanceStatus.Create("LookingForHome").Value,
                DateTime.Now,
                null
            ).Value;

            // Act
            var result = volunteer.AddPet(pet);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(1, pet.SerialNumber.Value);
        }

        [Fact]
        public void AddPet_ShouldAssignSerialNumbers_WhenMultiplePetsAreAdded()
        {
            // Arrange
            var volunteer = Volunteer.Create(
                VolunteerId.Create(Guid.NewGuid()),
                FullName.Create("John", "Doe", null).Value,
                Description.Create("Volunteer description").Value,
                YearsExperience.Create(5).Value,
                PhoneNumber.Create("1234567890").Value,
                null,
                null
            ).Value;

            var pet1 = Pet.Create(
                PetId.NewPetId(),
                Nickname.Create("Buddy").Value,
                SpeciesAndBreed.Create(SpeciesId.Create(Guid.NewGuid()), Guid.NewGuid()).Value,
                Description.Create("Pet description").Value,
                Color.Create("Brown").Value,
                HealthInformation.Create("Healthy").Value,
                Address.Create("City", "Street", "House", null, null).Value,
                Size.Create(50, 20).Value,
                PhoneNumber.Create("0987654321").Value,
                true,
                DateTime.Now,
                true,
                AssistanceStatus.Create("LookingForHome").Value,
                DateTime.Now,
                null
            ).Value;

            var pet2 = Pet.Create(
                PetId.NewPetId(),
                Nickname.Create("Max").Value,
                SpeciesAndBreed.Create(SpeciesId.Create(Guid.NewGuid()), Guid.NewGuid()).Value,
                Description.Create("Pet description").Value,
                Color.Create("Black").Value,
                HealthInformation.Create("Healthy").Value,
                Address.Create("City", "Street", "House", null, null).Value,
                Size.Create(55, 25).Value,
                PhoneNumber.Create("0987654322").Value,
                true,
                DateTime.Now,
                true,
                AssistanceStatus.Create("LookingForHome").Value,
                DateTime.Now,
                null
            ).Value;

            // Act
            var result1 = volunteer.AddPet(pet1);
            var result2 = volunteer.AddPet(pet2);

            // Assert
            Assert.True(result1.IsSuccess);
            Assert.True(result2.IsSuccess);
            Assert.Equal(1, pet1.SerialNumber.Value);
            Assert.Equal(2, pet2.SerialNumber.Value);
        }
    }
}