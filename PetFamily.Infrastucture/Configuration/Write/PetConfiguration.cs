using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Constants = PetFamily.Domain.Shared.Constants;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Infrastucture.Extensions;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastucture.Configuration.Write
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

            builder.ComplexProperty(p => p.Nickname, pb =>
            {
                pb.IsRequired();

                pb.Property(n => n.Value)
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("nickname");
            });

            builder.ComplexProperty(p => p.SerialNumber, pb =>
            {
                pb.IsRequired(true);

                pb.Property(n => n.Value)
                    .IsRequired(true)
                    .HasColumnName("serial_number");
            });

            builder.ComplexProperty(p => p.SpeciesAndBreed, pb =>
            {
                pb.IsRequired();

                pb.Property(n => n.SpeciesId)
                    .HasConversion(
                        id => id.Value,
                        value => SpeciesId.Create(value))
                    .HasColumnName("species_id");

                pb.Property(n => n.BreedId)
                    .HasColumnName("breed_id");
            });

            builder.ComplexProperty(p => p.Description, pb =>
            {
                pb.Property(n => n!.Value)
                    .IsRequired(false)
                    .HasMaxLength(Description.MAX_HIGHT_DESCRIPTION_LENGTH)
                    .HasColumnName("description");
            });

            builder.ComplexProperty(p => p.Color, pb =>
            {

                pb.Property(n => n!.Value)
                    .IsRequired(false)
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("color");
            });

            builder.ComplexProperty(p => p.HealthInformation, pb =>
            {
                pb.Property(n => n!.Value)
                    .IsRequired(false)
                    .HasMaxLength(Domain.Shared.Constants.MAX_HIGHT_TEXT_LENGTH)
                    .HasColumnName("health_information");
            });

            builder.ComplexProperty(p => p.Address, ab =>
            {
                ab.Property(a => a.Сity)
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("city");

                ab.Property(a => a.Street)
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("street");

                ab.Property(a => a.House)
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("house");

                ab.Property(a => a.Flat)
                    .IsRequired(false)
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("flat");

                ab.Property(a => a.ApartmentNumber)
                   .IsRequired(false)
                   .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
                   .HasColumnName("apartment_number");
            });

            builder.ComplexProperty(p => p.Size, pb =>
            {
                pb.IsRequired();

                pb.Property(s => s.Height)
                    .HasColumnName("height");

                pb.Property(s => s.Weight)
                    .HasColumnName("height");
            });

            builder.ComplexProperty(p => p.PhoneNumber, pb =>
            {
                pb.IsRequired();

                pb.Property(a => a!.Number)
                    .HasMaxLength(PhoneNumber.MAX_HIGHT_PHONE_NUMBER_LENGTH)
                    .HasColumnName("phone_number");
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
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("status");
            });

            builder.Property(p => p.DateOfCreation)
                    .HasColumnName("date_of_creation");

            builder.Property(p => p.DetailsForAssistance)
                .ValueObjectCollectionJsonConversion(
                    detailForAssistance => new DetailsForAssistanceDto { Name = detailForAssistance.Name, Description = detailForAssistance.Description },
                    dto => DetailsForAssistance.Create(dto.Name, dto.Description).Value)
                .HasColumnName("details_for_assistance");


            builder.HasMany(p => p.PetPhotos)
                .WithOne()
                .HasForeignKey("pet_id");

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
        }
    }
}
