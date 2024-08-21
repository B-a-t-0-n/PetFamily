using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Shared;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Infrastucture.Configuration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pet");

            builder.HasKey(i => i.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => PetId.Create(value));

            builder.Property(p => p.Nickname)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("nickname");

            builder.Property(p => p.TypeOfAnimals)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("type_of_animals");

            builder.Property(p => p.Description)
                    .IsRequired(false)
                    .HasMaxLength(Constants.MAX_HIGHT_TEXT_LENGTH)
                    .HasColumnName("description");

            builder.Property(p => p.BreedOfPet)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("breed_of_pet");

            builder.Property(p => p.Color)
                    .IsRequired(false)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("color");

            builder.Property(p => p.HealthInformation)
                    .IsRequired(false)
                    .HasMaxLength(Constants.MAX_HIGHT_TEXT_LENGTH)
                    .HasColumnName("health_information");

            builder.ComplexProperty(p => p.Address, ab =>
            {
                ab.IsRequired();

                ab.Property(a => a.Сity)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("city");

                ab.Property(a => a.Street)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("street");

                ab.Property(a => a.House)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("house");

                ab.Property(a => a.Flat)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("flat");
            });

            builder.Property(p => p.Weight)
                    .HasColumnName("weight");

            builder.Property(p => p.Height)
                    .HasColumnName("height");

            builder.ComplexProperty(p => p.PhoneNumber, pb =>
            {
                pb.IsRequired();

                pb.Property(a => a!.Number)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("phoneNumber");
            });

            builder.Property(p => p.IsCastrated)
                    .HasColumnName("is_castrated");

            builder.Property(p => p.DateOfBirth)
                    .IsRequired(false)
                    .HasColumnName("date_of_birth");

            builder.Property(p => p.IsVaccinated)
                    .HasColumnName("is_vaccinated");

            builder.ComplexProperty(p => p.AssistanceStatus, ab =>
            {
                ab.IsRequired();

                ab.Property(a => a.Status)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("status");
            });

            builder.Property(p => p.DateOfCreation)
                    .HasColumnName("date_of_creation");

            builder.OwnsOne(p => p.DetailsForAssistance, pb =>
            {
                pb.ToJson();

                pb.OwnsMany(d => d.DetailsForAssistance, db =>
                {
                    db.Property(i => i.Name)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    db.Property(i => i.Description)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_HIGHT_TEXT_LENGTH);
                });
            });

            builder.HasMany(p => p.PetPhotos)
                .WithOne()
                .HasForeignKey("pet_id");
        }
    }
}
