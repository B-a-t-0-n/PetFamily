using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Infrastucture.Configuration.Read
{
    public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
    {
        public void Configure(EntityTypeBuilder<PetDto> builder)
        {
            builder.ToTable("pet");

            builder.HasKey(i => i.Id);

            builder.HasMany(p => p.PetPhotos)
                .WithOne()
                .HasForeignKey(p => p.PetId);
        }
    }
}
