using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dtos;
using System.Text.Json;

namespace PetFamily.Volunteers.Infrastructure.Configuration.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pet");

        builder.HasKey(i => i.Id);

        builder.Property(v => v.Requisites)
            .HasConversion(
                detailsForAssistance => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<RequisitesDto[]>(json, JsonSerializerOptions.Default)!);
    }
}
