using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.SpeciesMenegment.Entity;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastucture.Configuration
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("species");

            builder.HasKey(i => i.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value));

            builder.ComplexProperty(s => s.Name, nb =>
            {
                nb.IsRequired();

                nb.Property(n => n.Value)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("name");
            });

            builder.HasMany(s => s.breeds)
                .WithOne()
                .HasForeignKey("breed_id");
        }
    }
}
