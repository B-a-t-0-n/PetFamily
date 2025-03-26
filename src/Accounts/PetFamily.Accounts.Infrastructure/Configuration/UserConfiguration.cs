using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Extentions;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel;
using Microsoft.AspNetCore.Identity;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.ComplexProperty(v => v.FullName, fnb =>
        {
            fnb.Property(f => f.Surname)
                .IsRequired(true)
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("surname");

            fnb.Property(f => f.FirstName)
                .IsRequired(true)
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("name");

            fnb.Property(f => f.Patronymic)
                .IsRequired(false)
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("patronymic");
        });

        builder
            .Property(v => v.SocialNetworks)
            .ValueObjectCollectionJsonConversion(
                socialNetwork => new SocialNetworkDto { Name = socialNetwork.Name, Link = socialNetwork.Link },
                dto => SocialNetwork.Create(dto.Name, dto.Link).Value)
            .HasColumnName("social_network");

        builder
            .HasMany(v => v.Roles)
            .WithMany()
            .UsingEntity<IdentityUserRole<Guid>>();

        builder
            .HasOne(v => v.AdminAccount)
            .WithOne(u => u.User)
            .HasForeignKey<AdminAccount>(a => a.UserId)
            .IsRequired(false);

        builder
            .HasOne(v => v.PartisipantAccount)
            .WithOne(u => u.User)
            .HasForeignKey<PartisipantAccount>(a => a.UserId)
            .IsRequired(false);

        builder
            .HasOne(v => v.VolunteerAccount)
            .WithOne(u => u.User)
            .HasForeignKey<VolunteerAccount>(a => a.UserId)
            .IsRequired(false);
    }
}
