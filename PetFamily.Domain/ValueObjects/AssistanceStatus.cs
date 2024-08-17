using CSharpFunctionalExtensions;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.ValueObjects
{
    public class AssistanceStatus : ValueObject
    {
        public static readonly AssistanceStatus NeedsHelp = new(nameof(NeedsHelp));
        public static readonly AssistanceStatus LookingForHome = new(nameof(LookingForHome));
        public static readonly AssistanceStatus FoundAHouse = new(nameof(FoundAHouse));

        private static readonly AssistanceStatus[] _all = [NeedsHelp, LookingForHome, FoundAHouse];

        private AssistanceStatus(string status)
        {
            Status = status;
        }

        public string Status { get; }

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
