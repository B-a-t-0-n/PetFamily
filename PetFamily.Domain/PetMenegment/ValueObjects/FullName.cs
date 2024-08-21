using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;
namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class FullName : ValueObject
    {
        private FullName() { }
        private FullName(string name, string surname, string patronymic)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }

        public string Name { get; } = default!;
        public string Surname { get; } = default!;
        public string Patronymic { get; } = default!;

        public static Result<FullName> Create(string name, string surname, string patronymic)
        {
            if (string.IsNullOrWhiteSpace(name))
                Result.Failure<FullName>("name is null or white space");

            if (string.IsNullOrWhiteSpace(surname))
                Result.Failure<FullName>("surname is null or white space");

            if (string.IsNullOrWhiteSpace(patronymic))
                Result.Failure<FullName>("patronymic is null or white space");

            var fullName = new FullName(name, surname, patronymic);

            return fullName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Surname;
            yield return Patronymic;
        }
    }
}
