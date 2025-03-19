using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Volunteers.Domain.Entity;

namespace PetFamily.Volunteers.Infrastructure.Configuration.Write;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteer");

        builder.HasKey(i => i.Id);

        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        builder.ComplexProperty(v => v.Description, db =>
        {
            db.Property(p => p!.Value)
                .IsRequired(false)
                .HasMaxLength(Description.MAX_HIGHT_DESCRIPTION_LENGTH)
                .HasColumnName("description");
        });

        builder.ComplexProperty(v => v.PhoneNumber, pnb =>
        {
            pnb.Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(PhoneNumber.MAX_HIGHT_PHONE_NUMBER_LENGTH)
                .HasColumnName("phone_number");
        });

        builder.HasMany(v => v.Pets)
            .WithOne()
            .IsRequired()
            .HasForeignKey("volunteer_id");

        builder.Property<bool>("IsDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");

        builder.Property<DateTime?>("DeletionDate")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("deletion_date");
    }
}
