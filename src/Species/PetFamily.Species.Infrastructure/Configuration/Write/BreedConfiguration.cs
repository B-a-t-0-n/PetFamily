using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Species.Domain.Entity;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.SharedKernel;


namespace PetFamily.Species.Infrastructure.Configuration.Write;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breed");

        builder.HasKey(i => i.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));

        builder.ComplexProperty(s => s.Name, nb =>
        {
            nb.IsRequired();

            nb.Property(n => n.Value)
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("name");
        });
    }
}
