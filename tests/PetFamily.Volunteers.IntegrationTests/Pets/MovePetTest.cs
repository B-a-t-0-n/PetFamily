using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.PetHandlers.MovePet.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Pets;

public class MovePetTest : VolunteersBaseTest
{
    public MovePetTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Move_pet()
    {
        // Arrange
        var volunteerId = await SeedVolunteer();

        var petIdFirst = await SeedPet(volunteerId);
        var petIdSecond = await SeedPet(volunteerId);

        var serialNumber = 2;

        var command = _fixture.MovePetCommand(volunteerId, petIdFirst, serialNumber);

        var cancellationToken = new CancellationTokenSource().Token;

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<int, MovePetCommand>>();

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var pet = await _volunteersReadDbContext.Pets.FirstOrDefaultAsync(v => v.Id == petIdFirst);

        pet.Should().NotBeNull();
        Assert.Equal(serialNumber, pet.SerialNumber);
    }
}
