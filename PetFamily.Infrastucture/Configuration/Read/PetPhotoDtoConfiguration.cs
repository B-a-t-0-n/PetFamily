using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastucture.Configuration.Read
{
    public class PetPhotoDtoConfiguration : IEntityTypeConfiguration<PetPhotoDto>
    {
        public void Configure(EntityTypeBuilder<PetPhotoDto> builder)
        {
            builder.ToTable("pet_photo");

            builder.HasKey(i => i.Id);
        }
    }
}
