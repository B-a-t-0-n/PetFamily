using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

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

        public static Result<Size, Error> Create(double height, double weight)
        {
            if (weight < 0)
                return Errors.General.ValueIsRequired("weight");

            if (height < 0)
                return Errors.General.ValueIsRequired("height");

            var size = new Size(height, weight);

            return size;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Height;
            yield return Weight;
        }
    }
}
