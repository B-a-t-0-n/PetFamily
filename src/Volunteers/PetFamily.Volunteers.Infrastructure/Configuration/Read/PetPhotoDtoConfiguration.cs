using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Infrastructure.Configuration.Read;

public class PetPhotoDtoConfiguration : IEntityTypeConfiguration<PetPhotoDto>
{
    public void Configure(EntityTypeBuilder<PetPhotoDto> builder)
    {
        builder.ToTable("pet_photo");

        builder.HasKey(i => i.Id);
    }
}
