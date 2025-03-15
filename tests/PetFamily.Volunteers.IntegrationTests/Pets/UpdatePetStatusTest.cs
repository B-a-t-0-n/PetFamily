using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.PetHandlers.UpdatePetStatus.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Pets;

public class UpdatePetStatusTest : VolunteersBaseTest
{
    public UpdatePetStatusTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Update_pet_status_in_database()
    {
        // Arrange
        var volunteerId = await SeedVolunteer();

        var petId = await SeedPet(volunteerId);

        var command = _fixture.UpdatePetStatusCommand(volunteerId, petId, "LookingForHome");

        var cancellationToken = new CancellationTokenSource().Token;

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, UpdatePetStatusCommand>>();

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var pet = await _volunteersReadDbContext.Pets.FirstOrDefaultAsync(v => v.Id == result.Value);

        pet.Should().NotBeNull();
    }
}