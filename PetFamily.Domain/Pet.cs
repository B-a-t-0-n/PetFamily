using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetFamily.Domain
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; } = default!;
        public string TypeOfAnimals { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string BreedOfPet { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string HealthInformation { get; set; } = default!;
        public string Address { get; set; } = default!;
        public double Weight { get; set; } = default!;
        public PhysicalQuantity PhysicalQuantityWeight { get; set; } = default!;
        public double Height { get; set; } = default!;
        public PhysicalQuantity PhysicalQuantityHeight { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public bool IsCastrated { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default!;
        public bool IsVaccinated { get; set; } = default!;
        public AssistanceStatus AssistanceStatus { get; set; } = default!;
        public DetailsForAssistance DetailsForAssistance { get; set; } = default!;
        public DateTime DateOfCreation { get; set; } = default!;

    }
}
