using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Infrastucture.Configuration
{
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

            builder.ComplexProperty(v => v.FullName, fnb =>
            {
                fnb.Property(f => f.Surname)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("surname");

                fnb.Property(f => f.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("name");

                fnb.Property(f => f.Patronymic)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("patronymic");
            });

            builder.ComplexProperty(v => v.Description, db =>
            {
                db.Property(p => p!.Value)
                    .IsRequired(false)
                    .HasMaxLength(Description.MAX_HIGHT_DESCRIPTION_LENGTH)
                    .HasColumnName("description");
            });

            builder.ComplexProperty(v => v.YearsExperience, yb =>
            {
                yb.Property(p => p!.Value)
                    .IsRequired()
                    .HasColumnName("years_experience");
            });

            builder.ComplexProperty(v => v.PhoneNumber, pnb =>
            {
                pnb.Property(p => p.Number)
                    .IsRequired()
                    .HasMaxLength(PhoneNumber.MAX_HIGHT_PHONE_NUMBER_LENGTH)
                    .HasColumnName("phone_number");
            });

            builder.OwnsOne(v => v.DetailsForAssistance, vb =>
            {
                vb.ToJson();

                vb.OwnsMany(s => s.DetailsForAssistance, sb =>
                {
                    sb.Property(i => i.Name)
                        .IsRequired(false)
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    sb.Property(i => i.Description)
                        .IsRequired(false)
                        .HasMaxLength(DetailsForAssistance.MAX_HIGHT_DESCRIPTION_LENGTH);
                });
            });

            builder.OwnsOne(v => v.SocialNetwork, vb =>
            {
                vb.ToJson();

                vb.OwnsMany(s => s.SocialNetwork, sb =>
                {
                    sb.Property(i => i.Name)
                        .IsRequired(false)
                        .HasMaxLength(SocialNetwork.MAX_HIGHT_NAME_LENGTH);

                    sb.Property(i => i.Link)
                        .IsRequired(false)
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                });
            });

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id");
        }
    }
}
