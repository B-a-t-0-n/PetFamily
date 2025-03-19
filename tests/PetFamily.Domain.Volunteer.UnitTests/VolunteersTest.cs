using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Volunteers.Domain.Entity;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Domain.Volunteer.UnitTests;

public class VolunteersTest
{

    private Volunteers.Domain.Entity.Volunteer CreateVolunteer()
    {
        return Volunteers.Domain.Entity.Volunteer.Create(
            VolunteerId.Create(Guid.NewGuid()),
            Description.Create("Volunteer description").Value,
            PhoneNumber.Create("1234567890").Value
        ).Value;
    }

    private Volunteers.Domain.Entity.Pet CreatePet(string nickname, string color, string phoneNumber)
    {
        return Pet.Create(
            PetId.NewPetId(),
            Nickname.Create(nickname).Value,
            SpeciesAndBreed.Create(SpeciesId.Create(Guid.NewGuid()), Guid.NewGuid()).Value,
            Description.Create("Pet description").Value,
            Color.Create(color).Value,
            HealthInformation.Create("Healthy").Value,
            Address.Create("City", "Street", "House", null, null).Value,
            Size.Create(50, 20).Value,
            PhoneNumber.Create(phoneNumber).Value,
            true,
            DateTime.Now,
            true,
            AssistanceStatus.Create("LookingForHome").Value,
            DateTime.Now,
            null!
        ).Value;
    }

    [Fact]
    public void AddPet_ShouldAssignSerialNumber_WhenFirstPetIsAdded()
    {
        // Arrange
        var volunteer = CreateVolunteer();
        var pet = CreatePet("Buddy", "Brown", "0987654321");

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
        var volunteer = CreateVolunteer();
        var pet1 = CreatePet("Buddy", "Brown", "0987654321");
        var pet2 = CreatePet("Max", "Black", "0987654322");

        // Act
        var result1 = volunteer.AddPet(pet1);
        var result2 = volunteer.AddPet(pet2);

        // Assert
        Assert.True(result1.IsSuccess);
        Assert.True(result2.IsSuccess);
        Assert.Equal(1, pet1.SerialNumber.Value);
        Assert.Equal(2, pet2.SerialNumber.Value);
    }

    [Fact]
    public void MovePet_SecondPetToSecondPosition_ReturnsSuccess()
    {
        // Arrange
        var volunteer = CreateVolunteer();

        var pets = new List<Pet>
        {
            CreatePet("Buddy", "Brown", "0987654321"),
            CreatePet("Max", "Black", "0987654322"),
            CreatePet("Charlie", "White", "0987654323"),
            CreatePet("Rocky", "Golden", "0987654324"),
            CreatePet("Jack", "Gray", "0987654325")
        };

        foreach (var pet in pets)
        {
            volunteer.AddPet(pet);
        }

        var secondPet = pets[1];
        var newSerialNumber = SerialNumber.Create(2).Value;

        // Act
        var result = volunteer.MovePet(secondPet, newSerialNumber);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, secondPet.SerialNumber.Value);
    }

    [Fact]
    public void MovePet_FourthPetToThirdPosition_FirstPetToLastPosition_ReturnsSuccess()
    {
        // Arrange
        var volunteer = CreateVolunteer();

        var pets = new List<Pet>
        {
            CreatePet("Buddy", "Brown", "0987654321"),
            CreatePet("Max", "Black", "0987654322"),
            CreatePet("Charlie", "White", "0987654323"),
            CreatePet("Rocky", "Golden", "0987654324"),
            CreatePet("Jack", "Gray", "0987654325")
        };

        foreach (var pet in pets)
        {
            volunteer.AddPet(pet);
        }

        var fourthPet = pets[3];
        var firstPet = pets[0];
        var newSerialNumberForFourthPet = SerialNumber.Create(3).Value;
        var newSerialNumberForFirstPet = SerialNumber.Create(pets.Count + 1).Value;

        // Act
        var result1 = volunteer.MovePet(fourthPet, newSerialNumberForFourthPet);
        var result2 = volunteer.MovePet(firstPet, newSerialNumberForFirstPet);

        // Assert
        Assert.True(result1.IsSuccess);
        Assert.Equal(2, fourthPet.SerialNumber.Value);

        Assert.True(result2.IsSuccess);
        Assert.Equal(pets.Count, firstPet.SerialNumber.Value);
    }

    [Fact]
    public void MovePet_FirstPetToLastPosition_ReturnsSuccess()
    {
        // Arrange
        var volunteer = CreateVolunteer();

        var pets = new List<Pet>
        {
            CreatePet("Buddy", "Brown", "0987654321"),
            CreatePet("Max", "Black", "0987654322"),
            CreatePet("Charlie", "White", "0987654323"),
            CreatePet("Rocky", "Golden", "0987654324"),
            CreatePet("Jack", "Gray", "0987654325")
        };

        foreach (var pet in pets)
        {
            volunteer.AddPet(pet);
        }

        var firstPet = pets[0];
        var newSerialNumber = SerialNumber.Create(pets.Count).Value;

        // Act
        var result = volunteer.MovePet(firstPet, newSerialNumber);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(pets.Count, firstPet.SerialNumber.Value);
    }

    [Fact]
    public void MovePet_LastPetToFirstPosition_ReturnsSuccess()
    {
        // Arrange
        var volunteer = CreateVolunteer();

        var pets = new List<Pet>
        {
            CreatePet("Buddy", "Brown", "0987654321"),
            CreatePet("Max", "Black", "0987654322"),
            CreatePet("Charlie", "White", "0987654323"),
            CreatePet("Rocky", "Golden", "0987654324"),
            CreatePet("Jack", "Gray", "0987654325")
        };

        foreach (var pet in pets)
        {
            volunteer.AddPet(pet);
        }

        var lastPet = pets[^1];
        var newSerialNumber = SerialNumber.Create(1).Value;

        // Act
        var result = volunteer.MovePet(lastPet, newSerialNumber);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, lastPet.SerialNumber.Value);
    }
}