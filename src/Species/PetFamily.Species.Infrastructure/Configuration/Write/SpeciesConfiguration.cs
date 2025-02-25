using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.SharedKernel;

namespace PetFamily.Species.Infrastructure.Configuration.Write;

public class SpeciesConfiguration : IEntityTypeConfiguration<Domain.Entity.Species>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Species> builder)
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
            .HasForeignKey("species_id");
    }
}
