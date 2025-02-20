namespace PetFamily.Application.Dtos
{
    public class PetDto
    {
        public Guid Id { get; init; }

        public string Nickname { get; init; } = string.Empty;

        public Guid? VolunteerId { get; init; } = default!;

        public Guid SpeciesId { get; init; } = default!;

        public Guid BreedId { get; init; } = default!;

        public string? Description { get; init; } = string.Empty;

        public string Color { get; init; } = string.Empty;

        public int SerialNumber { get; init; }

        public string? HealthInformation { get; init; } = string.Empty;

        public string City { get; init; } = string.Empty;

        public string Street { get; init; } = string.Empty;

        public string House { get; init; } = string.Empty;

        public string Flat { get; init; } = string.Empty;

        public string? ApartmentNumber { get; init; } = string.Empty;

        public double Height { get; init; }

        public double Weight { get; init; }

        public string PhoneNumber { get; init; } = string.Empty;

        public bool IsCastrated { get; init; }

        public DateTime DateOfBirth { get; init; }

        public bool IsVaccinated { get; init; }

        public string AssistanceStatus { get; init; } = string.Empty;

        public DateTime DateOfCreation { get; init; }

        public DetailsForAssistanceDto[] DetailsForAssistance { get; set; } = [];
    }
}
