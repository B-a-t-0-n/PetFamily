using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.PetHandlers.DeletePetPhoto.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Pets;

public class DeletePetPhotosTest : VolunteersBaseTest
{
    public DeletePetPhotosTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Delete_pet_photos()
    {
        // Arrange
        _factory.SetupSuccessFileProviderMock();

        var volunteerId = await SeedVolunteer();

        var petId = await SeedPet(volunteerId);

        var photo = await SeedPetPhoto(volunteerId, petId, "test.png");

        var command = _fixture.DeletePetPhotoCommand(volunteerId, petId, photo.Id);

        var cancellationToken = new CancellationTokenSource().Token;

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, DeletePetPhotoCommand>>();

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var volunteer = await _volunteersWriteDbContext.Volunteers
            .Include(v => v.Pets)
            .ThenInclude(p => p.PetPhotos)
            .FirstOrDefaultAsync(v => v.Id == volunteerId);
        volunteer.Should().NotBeNull();

        var pet = volunteer.Pets.FirstOrDefault(p => p.Id == petId);
        pet.Should().NotBeNull();

        var photoResult = pet.PetPhotos.FirstOrDefault();
        photoResult.Should().BeNull();
    }
}
