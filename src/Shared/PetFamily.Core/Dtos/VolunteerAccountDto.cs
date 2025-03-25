using PetFamily.SharedKernel.ValueObjects;
using static PetFamily.SharedKernel.Errors;

namespace PetFamily.Core.Dtos;

public class VolunteerAccountDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int YearsExperience { get; set; } = default!;

    public IEnumerable<RequisitesDto> Requisites { get; set; } = [];

    public IEnumerable<string> Certificates { get; set; } = [];
}