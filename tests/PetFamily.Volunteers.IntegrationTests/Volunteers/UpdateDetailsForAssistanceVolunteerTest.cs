using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateDetailsForAssistance.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Volunteers;

public class UpdateDetailsForAssistanceVolunteerTest : VolunteersBaseTest
{
    public UpdateDetailsForAssistanceVolunteerTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }
    [Fact]
    public async Task Update_details_for_assistance_in_database()
    {
        // Arrange
        var volunteerId = await SeedVolunteer();
        var command = _fixture.UpdateDetailsForAssistanceCommand(volunteerId);
        var cancellationToken = new CancellationTokenSource().Token;
        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, UpdateDetailsForAssistanceCommand>>();
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        // Assert
        result.IsSuccess.Should().BeTrue();
        var volunteer = await _volunteersReadDbContext.Volunteers.FirstOrDefaultAsync(v => v.Id == result.Value);
        volunteer.Should().NotBeNull();
    }
}
