using PetFamily.Domain.Shared;

namespace PetFamily.API.Response
{
    public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

    public class Envelope
    {
        private Envelope(object? rezult, IEnumerable<ResponseError> errors)
        {
            Rezult = rezult;
            Errors = errors.ToList();
            TimeGenerated = DateTime.Now;
        }

        public object? Rezult { get; }

        public List<ResponseError> Errors { get; }

        public DateTime TimeGenerated { get; }

        public static Envelope Ok(object? rezult = null) =>
            new(rezult, []);

        public static Envelope Error(IEnumerable<ResponseError> errors) => 
            new(null, errors);

    }
}
