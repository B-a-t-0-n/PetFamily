using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.SpeciesMenegment.Entity;
using PetFamily.Domain.Shared.IDs;


namespace PetFamily.Infrastucture.Configuration.Write
{
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
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("name");
            });
        }
    }
}
