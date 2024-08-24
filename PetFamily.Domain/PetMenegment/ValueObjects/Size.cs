using CSharpFunctionalExtensions;
using PetFamily.Domain.PetMenegment.Entity;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class Size : ValueObject
    {
        private Size() { }
        private Size(double height, double weight)
        {
            Height = height;
            Weight = weight;
        }

        public double Height { get; } = default!;
        public double Weight { get; } = default!;

        public static Result<Size> Create(double height, double weight)
        {
            if (weight < 0)
                Result.Failure<Size>("weight < 0");

            if (height < 0)
                Result.Failure<Size>("height < 0");

            var size = new Size(height, weight);

            return size;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Height;
            yield return Weight;
        }
    }
}
