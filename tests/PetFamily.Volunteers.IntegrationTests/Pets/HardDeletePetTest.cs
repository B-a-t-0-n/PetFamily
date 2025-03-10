using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.PetHandlers.HardDeletePet.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Pets;

public class HardDeletePetTest : VolunteersBaseTest
{
    public HardDeletePetTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Hard_delete_pet_in_database()
    {
        // Arrange
        var volunteerId = await SeedVolunteer();

        var petId = await SeedPet(volunteerId);

        var command = _fixture.HardDeletePetCommand(volunteerId, petId);

        var cancellationToken = new CancellationTokenSource().Token;

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<HardDeletePetCommand>>();

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var pet = await _volunteersReadDbContext.Pets.FirstOrDefaultAsync();

        pet.Should().BeNull();
    }
}
