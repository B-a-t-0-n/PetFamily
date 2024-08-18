using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Entity;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastucture.Configuration
{
    public class PetPhotoConfiguration : IEntityTypeConfiguration<PetPhoto>
    {
        public void Configure(EntityTypeBuilder<PetPhoto> builder)
        {
            builder.ToTable("pet_photo");

            builder.HasKey(i => i.Id);

            builder.Property(p => p.IsMain)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("is_main");

            builder.Property(p => p.Path)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("path");
        }
    }
}
