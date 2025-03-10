using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dtos;
using PetFamily.Core.Extentions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Volunteers.Domain.Entity;
using PetFamily.Volunteers.Domain.ValueObjects;

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
