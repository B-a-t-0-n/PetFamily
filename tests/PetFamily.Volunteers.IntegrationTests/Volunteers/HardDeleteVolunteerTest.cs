using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.HardDelete.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Volunteers;

public class HardDeleteVolunteerTest : VolunteersBaseTest
{
    public HardDeleteVolunteerTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Delete_volunteer_in_database()
    {
        // Arrange
        var volunteerId = await SeedVolunteer();

        var command = _fixture.HardDeleteVolunteerCommand(volunteerId);

        var cancellationToken = new CancellationTokenSource().Token;

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<HardDeleteVolunteerCommand>>();

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        
        var volunteer = await _volunteersReadDbContext.Volunteers.FirstOrDefaultAsync();

        volunteer.Should().BeNull();
    }
}
