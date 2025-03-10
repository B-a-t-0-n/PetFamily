using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.SoftDelete.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Volunteers;

public class SoftDeleteVolunteerTest : VolunteersBaseTest
{
    public SoftDeleteVolunteerTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Soft_delete_volunteer_in_database()
    {
        // Arrange
        var volunteerId = await SeedVolunteer();

        var command = _fixture.SoftDeleteVolunteerCommand(volunteerId);

        var cancellationToken = new CancellationTokenSource().Token;

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, SoftDeleteVolunteerCommand>>();

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var volunteer = await _volunteersReadDbContext.Volunteers.FirstOrDefaultAsync(v => v.Id == result.Value);

        volunteer.Should().NotBeNull();
    }
}
