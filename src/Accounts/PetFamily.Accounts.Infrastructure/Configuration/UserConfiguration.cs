using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Extentions;
using PetFamily.Volunteers.Domain.ValueObjects;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.UseTptMappingStrategy();

        builder.ToTable("users");

        builder.HasKey(u => u.Id);

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

        builder
            .Property(v => v.SocialNetworks)
            .ValueObjectCollectionJsonConversion(
                socialNetwork => new SocialNetworkDto { Name = socialNetwork.Name, Link = socialNetwork.Link },
                dto => SocialNetwork.Create(dto.Name, dto.Link).Value)
            .HasColumnName("social_network");
    }
}
