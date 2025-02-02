using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Application.Dtos
{
    public class VolunteerDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public string Surname { get; init; } = string.Empty;

        public string? Patronymic { get; init; } = string.Empty;

        public string? Description { get; init; } = string.Empty;

        public int YearsExperience { get; init; }

        public string PhoneNumber { get; init; } = string.Empty;

        //public ValueObjectList<SocialNetwork>? SocialNetwork { get; private set; }

        //public ValueObjectList<DetailsForAssistance>? DetailsForAssistance { get; private set; } = default!;

        public PetDto[] Pets { get; init; } = [];
    }
}
