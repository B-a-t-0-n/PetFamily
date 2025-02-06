using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Infrastucture.Extensions;

namespace PetFamily.Infrastucture.Configuration.Write
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
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("surname");

                fnb.Property(f => f.Name)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("name");

                fnb.Property(f => f.Patronymic)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
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

            builder.Property(v => v.DetailsForAssistance)
                .ValueObjectCollectionJsonConversion(
                    detailForAssistance => new DetailsForAssistanceDto { Name = detailForAssistance.Name, Description = detailForAssistance.Description },
                    dto => DetailsForAssistance.Create(dto.Name, dto.Description).Value)
                .HasColumnName("details_for_assistance");

            builder.Property(v => v.SocialNetwork)
                .ValueObjectCollectionJsonConversion(
                    socialNetwork => new SocialNetworkDto { Name = socialNetwork.Name, Link = socialNetwork.Link },
                    dto => SocialNetwork.Create(dto.Name, dto.Link).Value)
                .HasColumnName("social_network");

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id");

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
        }
    }
}
