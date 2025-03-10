using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.PetHandlers.UpdateInfoPet.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Pets;

public class UpdateInfoPetTest : VolunteersBaseTest
{
    public UpdateInfoPetTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Update_info_pet_in_database()
    {
        // Arrange
        _factory.SetupSuccessSpeciesContractMock();

        var volunteerId = await SeedVolunteer();

        var petId = await SeedPet(volunteerId);

        var command = _fixture.UpdatePetInfoCommand(volunteerId, petId);

        var cancellationToken = new CancellationTokenSource().Token;

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, UpdatePetInfoCommand>>();

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var pet = await _volunteersReadDbContext.Pets.FirstOrDefaultAsync(v => v.Id == result.Value);

        pet.Should().NotBeNull();
    }
}
