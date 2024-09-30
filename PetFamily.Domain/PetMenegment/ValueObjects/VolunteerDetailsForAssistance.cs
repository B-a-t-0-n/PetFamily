namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public record VolunteerDetailsForAssistance
    {
        private VolunteerDetailsForAssistance() { }
        public VolunteerDetailsForAssistance(IReadOnlyList<DetailsForAssistance>? detailsForAssistance)
        {
            DetailsForAssistance = detailsForAssistance;
        }

        public IReadOnlyList<DetailsForAssistance>? DetailsForAssistance { get; }
    }
}
