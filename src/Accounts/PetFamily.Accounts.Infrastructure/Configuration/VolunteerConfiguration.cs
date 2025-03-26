using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Extentions;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Infrastructure.Configuration;

public class VolunteerConfiguration : IEntityTypeConfiguration<VolunteerAccount>
{
    public void Configure(EntityTypeBuilder<VolunteerAccount> builder)
    {
        builder.ToTable("volunteer_accounts");

        builder.ComplexProperty(v => v.YearsExperience, yb =>
        {
            yb.Property(p => p!.Value)
                .IsRequired()
                .HasColumnName("years_experience");
        });

        builder
            .Property(v => v.Requisites)
            .ValueObjectCollectionJsonConversion(
                requisites => new RequisitesDto { Name = requisites.Name, Description = requisites.Description },
                dto => Requisites.Create(dto.Name, dto.Description).Value)
            .HasColumnName("requisites");

        builder
            .Property(v => v.Certificates)
            .ValueObjectCollectionJsonConversion(
                certificates => certificates,
                dto => dto)
            .HasColumnName("certificates"); ;

        builder
            .HasOne(v => v.User)
            .WithOne(u => u.VolunteerAccount)
            .HasForeignKey<VolunteerAccount>(v => v.UserId);
    }
}
