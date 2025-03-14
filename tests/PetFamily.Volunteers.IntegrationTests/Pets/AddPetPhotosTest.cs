using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Application.Commands.PetHandlers.AddPetPhotos.Commands;

namespace PetFamily.Volunteers.IntegrationTests.Pets;

public class AddPetPhotosTest : VolunteersBaseTest
{
    public AddPetPhotosTest(IntegrationTestsWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Add_pet_photos()
    {
        // Arrange
        _factory.SetupSuccessFileProviderMock();

        var volunteerId = await SeedVolunteer();

        var petId = await SeedPet(volunteerId);

        //здесь должен быть файл .png
        using var fileStream = new FakeImageStream();

        var command = new AddPetPhotosCommand
            (volunteerId,
            petId,
            new List<CreateFileDto>
            {
                new CreateFileDto(fileStream, "test.png")
            });

        var cancellationToken = new CancellationTokenSource().Token;

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<IReadOnlyList<PhotoPath>, AddPetPhotosCommand>>();

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

        var photo = pet.PetPhotos.FirstOrDefault();
        photo.Should().NotBeNull();
    }
}
