using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateMainInfo.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Volunteers;

public class UpdateMainInfoVolunteerTest : VolunteersBaseTest
{
    public UpdateMainInfoVolunteerTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Update_main_info_in_database()
    {
        // Arrange
        var volunteerId = await SeedVolunteer();

        var command = _fixture.UpdateMainInfoCommand(volunteerId);

        var cancellationToken = new CancellationTokenSource().Token;

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, UpdateMainInfoCommand>>();

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var volunteer = await _volunteersReadDbContext.Volunteers.FirstOrDefaultAsync(v => v.Id == result.Value);

        volunteer.Should().NotBeNull();
    }
}
