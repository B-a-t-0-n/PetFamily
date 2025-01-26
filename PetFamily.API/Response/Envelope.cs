using PetFamily.Domain.Shared;

namespace PetFamily.API.Response
{
    public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

    public class Envelope
    {
        private Envelope(object? rezult, ErrorList? errors)
        {
            Rezult = rezult;
            Errors = errors;
            TimeGenerated = DateTime.Now;
        }

        public object? Rezult { get; }

        public ErrorList? Errors { get; }

        public DateTime TimeGenerated { get; }

        public static Envelope Ok(object? rezult = null) =>
            new(rezult, null);

        public static Envelope Error(ErrorList errors) => 
            new(null, errors);

    }
}
