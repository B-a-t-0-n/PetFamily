using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.Create.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Volunteers;

public class AddVolunteerTests : VolunteersBaseTest
{
    public AddVolunteerTests(IntegrationTestsWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_volunteer_to_database()
    {
        // Arrange
        var command = _fixture.CreateVolunteerCommand();

        var cancellationToken = new CancellationTokenSource().Token;

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, CreateVolunteerCommand>>();

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();

        var volunteer = await _volunteersReadDbContext.Volunteers.FirstOrDefaultAsync(v => v.Id == result.Value);

        volunteer.Should().NotBeNull();
    }
}