namespace PetFamily.Application.Dtos
{
    public class VolunteerDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public string Surname { get; init; } = string.Empty;

        public string? Patronymic { get; init; } = string.Empty;

        public string? Description { get; init; }

        public int YearsExperience { get; init; }

        public string PhoneNumber { get; init; } = string.Empty;

        public SocialNetworkDto[] SocialNetwork { get; set; } = [];

        public DetailsForAssistanceDto[] DetailsForAssistance { get; set; } = [];
    }
}
