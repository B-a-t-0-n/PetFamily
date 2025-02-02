using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Infrastucture.Configuration.Read
{
    public class VolunteerDtoConfiguretion : IEntityTypeConfiguration<VolunteerDto>
    {
        public void Configure(EntityTypeBuilder<VolunteerDto> builder)
        {
            builder.ToTable("volunteer");

            builder.HasKey(i => i.Id);

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey(v => v.VolunteerId);
        }
    }
}
