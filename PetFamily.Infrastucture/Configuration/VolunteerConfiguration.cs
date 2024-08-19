﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entity;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

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

            builder.Property(v => v.Description)
                    .HasMaxLength(Constants.MAX_HIGHT_TEXT_LENGTH)
                    .HasColumnName("description");

            builder.Property(v => v.YearsExperience)
                    .HasColumnName("years_experience");

            builder.ComplexProperty(v => v.NumberPets, npb =>
            {
                npb.Property(n => n.BeingTreated)
                    .IsRequired()
                    .HasDefaultValue(0)
                    .HasColumnName("being_treated");

                npb.Property(n => n.FoundAHouse)
                    .IsRequired()
                    .HasDefaultValue(0)
                    .HasColumnName("found_a_house");

                npb.Property(n => n.LookingForHouse)
                    .IsRequired()
                    .HasDefaultValue(0)
                    .HasColumnName("looking_for_house");
            });

            builder.ComplexProperty(v => v.PhoneNumber, pnb =>
            {
                pnb.Property(p => p.Number)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("phone_number");
            });

            builder.ComplexProperty(v => v.DetailsForAssistance, db =>
            {
                db.IsRequired();

                db.Property(d => d!.Name)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("name_details_for_assistance");

                db.Property(d => d!.Description)
                   .HasMaxLength(Constants.MAX_HIGHT_TEXT_LENGTH)
                   .HasColumnName("description_details_for_assistance");
            });

            builder.OwnsOne(v => v.SocialNetwork, vb =>
            {
                vb.ToJson();

                vb.OwnsMany(s => s.SocialNetwork, sb =>
                {
                    sb.Property(i => i.Name)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    sb.Property(i => i.Link)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                });
            });

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id");
        }
    }
}
