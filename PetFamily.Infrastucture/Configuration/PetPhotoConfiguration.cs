using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Shared;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Infrastucture.Configuration
{
    public class PetPhotoConfiguration : IEntityTypeConfiguration<PetPhoto>
    {
        public void Configure(EntityTypeBuilder<PetPhoto> builder)
        {
            builder.ToTable("pet_photo");

            builder.HasKey(i => i.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => PetPhotoId.Create(value));

            builder.Property(p => p.IsMain)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("is_main");

            builder.ComplexProperty(p => p.Path, pb =>
            {
                pb.IsRequired();

                pb.Property(n => n.PathToStorage)
                    .HasMaxLength(Constants.MAX_HIGHT_TEXT_LENGTH)
                    .HasColumnName("path_to_storage");
            });
        }
    }
}
