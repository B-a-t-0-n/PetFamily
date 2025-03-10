using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateSocialNetwork.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Volunteers;

public class UpdateSocialNetworksVolunteerTest : VolunteersBaseTest
{
    public UpdateSocialNetworksVolunteerTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }
    [Fact]
    public async Task Update_social_networks_in_database()
    {
        // Arrange
        var volunteerId = await SeedVolunteer();
        var command = _fixture.UpdateSocialNetworkCommand(volunteerId);
        var cancellationToken = new CancellationTokenSource().Token;
        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, UpdateSocialNetworkCommand>>();
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        // Assert
        result.IsSuccess.Should().BeTrue();
        var volunteer = await _volunteersReadDbContext.Volunteers.FirstOrDefaultAsync(v => v.Id == result.Value);
        volunteer.Should().NotBeNull();
    }
}
