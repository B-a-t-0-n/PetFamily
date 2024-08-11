using CSharpFunctionalExtensions;

namespace PetFamily.Domain
{
    public class Pet
    {
        //ef core
        private Pet()
        {

        }

        private Pet(string s)
        {

        }

        public Guid Id { get; protected set; }

        public string Nickname { get; protected set; } = default!;

        public string TypeOfAnimals { get; protected set; } = default!;

        public string Description { get; protected set; } = default!;

        public string BreedOfPet { get; protected set; } = default!;

        public string Color { get; protected set; } = default!;

        public string HealthInformation { get; protected set; } = default!;

        public string Address { get; protected set; } = default!;

        public double Weight { get; protected set; } = default!;

        public PhysicalQuantity PhysicalQuantityWeight { get; protected set; } = default!;

        public double Height { get; protected set; } = default!;

        public PhysicalQuantity PhysicalQuantityHeight { get; protected set; } = default!;

        public string PhoneNumber { get; protected set; } = default!;

        public bool IsCastrated { get; protected set; } = default!;

        public DateTime DateOfBirth { get; protected set; } = default!;

        public bool IsVaccinated { get; protected set; } = default!;

        public AssistanceStatus AssistanceStatus { get; protected set; } = default!;

        public DetailsForAssistance DetailsForAssistance { get; protected set; } = default!;

        public DateTime DateOfCreation { get; protected set; } = default!;

        public static Result<Pet> Create()
        {
            //validation

            var pet = new Pet();

            return Result.Success(pet);
        }
    }
}
