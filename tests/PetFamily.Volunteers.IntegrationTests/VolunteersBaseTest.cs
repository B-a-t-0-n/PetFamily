
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Domain.Entity;
using PetFamily.Volunteers.Domain.ValueObjects;
using PetFamily.Volunteers.Infrastructure.DbContexts;

namespace PetFamily.Volunteers.IntegrationTests;

public class VolunteersBaseTest : IClassFixture<IntegrationTestsWebFactory>, IAsyncLifetime
{
    protected readonly IntegrationTestsWebFactory _factory;
    protected readonly Fixture _fixture;
    protected readonly IServiceScope _scope;
    protected readonly WriteVolunteersDbContext _volunteersWriteDbContext;
    protected readonly IReadVolunteersDbContext _volunteersReadDbContext;

    public VolunteersBaseTest(IntegrationTestsWebFactory factory)
    {
        _factory = factory;
        _fixture = new Fixture();
        _scope = factory.Services.CreateScope();
        _volunteersWriteDbContext = _scope
            .ServiceProvider.GetRequiredService<WriteVolunteersDbContext>();
        _volunteersReadDbContext = _scope
            .ServiceProvider.GetRequiredService<IReadVolunteersDbContext>();
    }

    protected async Task<Guid> SeedVolunteer()
    {
        var result = Domain.Entity.Volunteer.Create(
            VolunteerId.NewVolunteerId(),
            FullName.Create("name", "surname", "patronomic").Value,
            Description.Create("Description").Value,
            YearsExperience.Create(1).Value,
            PhoneNumber.Create("1234567890").Value,
            new List<DetailsForAssistance>(),
            new List<SocialNetwork>());
        if(result.IsFailure)
            throw new Exception("Volunteer not created");

        await _volunteersWriteDbContext.Volunteers.AddAsync(result.Value);

        await _volunteersWriteDbContext.SaveChangesAsync();

        return result.Value.Id;
    }

    protected async Task<Guid> SeedPet(Guid volunteerId)
    {
        var volunteer = await _volunteersWriteDbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId) ??
            throw new Exception("Volunteer not found");

        var pet = Pet.Create(
            PetId.NewPetId(),
            Nickname.Create("test").Value,
            SpeciesAndBreed.Create(SpeciesId.NewSpeciesId(), Guid.NewGuid()).Value,
            Description.Create("test").Value,
            Color.Create("test").Value,  
            HealthInformation.Create("test").Value,
            Address.Create("test", "test" , "test", "test", "test").Value,
            Size.Create(1, 1).Value,
            PhoneNumber.Create("1234567890").Value,
            false,
            DateTime.UtcNow,
            false,
            AssistanceStatus.Create("needshelp").Value,
            DateTime.UtcNow,
            new List<DetailsForAssistance>()
            );
        if (pet.IsFailure)
            throw new Exception("pet not created");

        var result = volunteer.AddPet(pet.Value);
        if (result.IsFailure)
            throw new Exception("Pet not added to volunteer");

        await _volunteersWriteDbContext.SaveChangesAsync();

        return pet.Value.Id;
    }

    protected async Task<PetPhoto> SeedPetPhoto(
        Guid volunteerId,
        Guid petId,
        string path)
    {
        var volunteer = await _volunteersWriteDbContext.Volunteers
           .Include(v => v.Pets)
           .FirstOrDefaultAsync(v => v.Id == volunteerId) ??
           throw new Exception("Volunteer not found");

        var pet = volunteer.Pets.FirstOrDefault(p => p.Id == petId) ??
            throw new Exception("Pet not found");

        var photo = PetPhoto.Create(
            PetPhotoId.NewPetPhotoId(),
            PhotoPath.Create(path).Value,
            false
            );
        if (photo.IsFailure)
            throw new Exception("Photo not created");

        var result = volunteer.AddPetPhoto(pet.Id, photo.Value);
        if (result.IsFailure)
            throw new Exception("Photo not added to pet");  

        await _volunteersWriteDbContext.SaveChangesAsync();

        return photo.Value;
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        _scope.Dispose();
        await _factory.ResetDatabaseAsync();
    }
}

