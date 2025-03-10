using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.PetHandlers.AddPet.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Pets;

public class AddPetTest : VolunteersBaseTest
{
    public AddPetTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Add_pet_in_database()
    {
        // Arrange
        _factory.SetupSuccessSpeciesContractMock();

        var volunteerId = await SeedVolunteer();

        var command = _fixture.AddPetCommand(volunteerId, Guid.NewGuid(), Guid.NewGuid());

        var cancellationToken = new CancellationTokenSource().Token;

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, AddPetCommand>>();

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var pet = await _volunteersReadDbContext.Pets.FirstOrDefaultAsync(v => v.Id == result.Value);
        pet.Should().NotBeNull();
    }
}
