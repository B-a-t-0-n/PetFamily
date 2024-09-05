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

        public static Result<FullName, Error> Create(string name, string surname, string? patronymic)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValueIsInvalid("name");

            if (name.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsRequired("name");

            if (string.IsNullOrWhiteSpace(surname))
                return Errors.General.ValueIsInvalid("surname");

            if (surname.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsRequired("surname");

            if (patronymic != null && patronymic!.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsRequired("patronymic");

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
