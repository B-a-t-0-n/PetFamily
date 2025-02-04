using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using System.Text.Json;

namespace PetFamily.Infrastucture.Configuration.Read
{
    public class VolunteerDtoConfiguretion : IEntityTypeConfiguration<VolunteerDto>
    {
        public void Configure(EntityTypeBuilder<VolunteerDto> builder)
        {
            builder.ToTable("volunteer");

            builder.HasKey(i => i.Id);

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey(v => v.VolunteerId);

            builder.Property(v => v.DetailsForAssistance)
                .HasConversion(
                    detailsForAssistance => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                    json => JsonSerializer.Deserialize<DetailsForAssistanceDto[]>(json, JsonSerializerOptions.Default)!);

            builder.Property(v => v.SocialNetwork)
                .HasConversion(
                    socialNetwork => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                    json => JsonSerializer.Deserialize<SocialNetworkDto[]>(json, JsonSerializerOptions.Default)!);
        }
    }
}
