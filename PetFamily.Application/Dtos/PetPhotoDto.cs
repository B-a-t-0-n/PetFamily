namespace PetFamily.Application.Dtos
{
    public class PetPhotoDto
    {
        public Guid Id { get; init; }

        public Guid PetId { get; init; }

        public string PhotoPath { get; init; } = string.Empty;

        public bool IsMain { get; init; }
    }
}
