using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class AssistanceStatus : ValueObject
    {
        private static readonly AssistanceStatus[] _all = [NeedsHelp!, LookingForHome!, FoundAHouse!];

        private AssistanceStatus() { }
        private AssistanceStatus(string status)
        {
            Status = status;
        }

        public static readonly AssistanceStatus NeedsHelp = new(nameof(NeedsHelp));
        public static readonly AssistanceStatus LookingForHome = new(nameof(LookingForHome));
        public static readonly AssistanceStatus FoundAHouse = new(nameof(FoundAHouse));

        public string Status { get; } = default!;

        public static Result<AssistanceStatus> Create(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                Result.Failure<AssistanceStatus>("status is null or white space");

            var statusInput = status.Trim().ToLower();

            if (_all.Any(s => s.Status.ToLower() == statusInput) == false)
                Result.Failure<AssistanceStatus>("error status");

            var assistanceStatus = new AssistanceStatus(statusInput);

            return assistanceStatus;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Status;
        }
    }
}
