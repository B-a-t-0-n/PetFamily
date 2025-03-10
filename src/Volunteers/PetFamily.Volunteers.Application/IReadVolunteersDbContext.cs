using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application;

public interface IReadVolunteersDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetDto> Pets { get; }
    IQueryable<PetPhotoDto> PetPhotos { get; }
}
