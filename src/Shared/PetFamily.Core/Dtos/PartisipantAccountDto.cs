
namespace PetFamily.Core.Dtos;

public class PartisipantAccountDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public IEnumerable<Guid> FavoritePets { get; set; } = [];
}