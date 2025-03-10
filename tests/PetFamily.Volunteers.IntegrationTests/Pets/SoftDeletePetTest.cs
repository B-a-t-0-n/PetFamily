using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.PetHandlers.SoftDeletePet.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Pets;

public class SoftDeletePetTest : VolunteersBaseTest
{
    public SoftDeletePetTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Soft_delete_pet_in_database()
    {
        // Arrange
        var volunteerId = await SeedVolunteer();

        var petId = await SeedPet(volunteerId);

        var command = _fixture.SoftDeletePetCommand(volunteerId, petId);

        var cancellationToken = new CancellationTokenSource().Token;

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, SoftDeletePetCommand>>();

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var pet = await _volunteersReadDbContext.Pets.FirstOrDefaultAsync(v => v.Id == result.Value);

        pet.Should().NotBeNull();
    }
}
