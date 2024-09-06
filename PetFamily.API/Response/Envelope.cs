using PetFamily.Domain.Shared;

namespace PetFamily.API.Response
{
    public class Envelope
    {
        private Envelope(object? rezult, Error? error)
        {
            Rezult = rezult;
            ErrorCode = error?.Code;
            ErrorMessage = error?.Message;
            TimeGenerated = DateTime.Now;
        }

        public object? Rezult { get; }

        public string? ErrorCode { get; }

        public string? ErrorMessage { get; }

        public DateTime TimeGenerated { get; }

        public static Envelope Ok(object? rezult = null) =>
            new(rezult, null);

        public static Envelope Error(Error error) => 
            new(null, error);

    }
}
