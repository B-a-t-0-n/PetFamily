using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class FullName : ValueObject
    {
        private FullName() { }
        private FullName(string name, string surname, string? patronymic)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }

        public string Name { get; } = default!;
        public string Surname { get; } = default!;
        public string? Patronymic { get; } = default!;

        public static Result<FullName> Create(string name, string surname, string? patronymic)
        {
            if (string.IsNullOrWhiteSpace(name))
                Result.Failure<FullName>("name is null or white space");

            if (name.Length > Constants.MAX_LOW_TEXT_LENGTH)
                Result.Failure<FullName>($"name > {Constants.MAX_LOW_TEXT_LENGTH}");

            if (string.IsNullOrWhiteSpace(surname))
                Result.Failure<FullName>("surname is null or white space");

            if (surname.Length > Constants.MAX_LOW_TEXT_LENGTH)
                Result.Failure<FullName>($"surname > {Constants.MAX_LOW_TEXT_LENGTH}");

            if (patronymic != null && patronymic!.Length > Constants.MAX_LOW_TEXT_LENGTH)
                Result.Failure<FullName>($"patronymic > {Constants.MAX_LOW_TEXT_LENGTH}");

            var fullName = new FullName(name, surname, patronymic);

            return fullName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Surname;
            yield return Patronymic == null ? "" : Patronymic;
        }
    }
}
