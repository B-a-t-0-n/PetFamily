namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public record PetDetailsForAssistance
    {
        private PetDetailsForAssistance() { }
        public PetDetailsForAssistance(IReadOnlyList<DetailsForAssistance> detailsForAssistance)
        {
            DetailsForAssistance = detailsForAssistance;
        }

        public IReadOnlyList<DetailsForAssistance> DetailsForAssistance { get; } = default!;

        
    }
}
