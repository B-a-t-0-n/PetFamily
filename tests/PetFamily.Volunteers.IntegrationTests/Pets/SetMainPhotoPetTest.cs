using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.PetHandlers.SetMainPhotoPet.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Pets;

public class SetMainPhotoPetTest : VolunteersBaseTest
{
    public SetMainPhotoPetTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }
    [Fact]
    public async Task Set_main_photo_pet()
    {
        // Arrange
        _factory.SetupSuccessFileProviderMock();

        var volunteerId = await SeedVolunteer();

        var petId = await SeedPet(volunteerId);

        var photo = await SeedPetPhoto(volunteerId, petId, "test.png");
        
        var command = _fixture.SetMainPhotoPetCommand(volunteerId, petId, photo.Path.PathToStorage);
        
        var cancellationToken = new CancellationTokenSource().Token;
        
        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<SetMainPhotoPetCommand>>();
        
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
        
        photoResult.Should().NotBeNull();
        
        photoResult.IsMain.Should().BeTrue();
    }
}