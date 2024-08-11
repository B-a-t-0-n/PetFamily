using CSharpFunctionalExtensions;
using PetFamily.Domain.ValueObjects;

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

        public string? Description { get; protected set; }

        public string BreedOfPet { get; protected set; } = default!;

        public string? Color { get; protected set; }

        public string? HealthInformation { get; protected set; }

        public string Address { get; protected set; } = default!;

        public PhysicalQuantity Weight { get; protected set; } = default!;

        public PhysicalQuantity Height { get; protected set; } = default!;

        public PhoneNumber? PhoneNumber { get; protected set; } 

        public bool IsCastrated { get; protected set; }

        public DateTime? DateOfBirth { get; protected set; }

        public bool IsVaccinated { get; protected set; }

        public AssistanceStatus AssistanceStatus { get; protected set; } = default!;

        public DetailsForAssistance DetailsForAssistance { get; protected set; } = default!;

        public DateTime DateOfCreation { get; protected set; }

        public static Result<Pet> Create()
        {
            //validation

            var pet = new Pet();

            return Result.Success(pet);
        }
    }
}
