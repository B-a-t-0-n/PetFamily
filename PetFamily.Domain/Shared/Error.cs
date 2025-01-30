namespace PetFamily.Domain.Shared
{
    public record Error
    {
        public const string SEPARATOR = "||";

        private Error(string code, string message, ErrorType type, string? invalidField = null)
        {
            Code = code;
            Message = message;
            Type = type;
            InvalidField = invalidField;
        }

        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }
        public string? InvalidField { get; } = null;

        public static Error Validation(string code, string message, string? invalidField = null) =>
            new Error(code, message, ErrorType.Validation, invalidField);

        public static Error NotFound(string code, string message) =>
            new Error(code, message, ErrorType.NotFound);

        public static Error Failure(string code, string message) =>
            new Error(code, message, ErrorType.Failure);

        public static Error Conflict(string code, string message) =>
            new Error(code, message, ErrorType.Conflict);

        public string Serialize()
        {
            return string.Join(SEPARATOR, Code, Message, Type);
        }

        public static Error Deserialize(string serialized)
        {
            var parts = serialized.Split(SEPARATOR);

            if (parts.Length < 2)
                throw new ArgumentException("invalid serialised format");

            if (Enum.TryParse<ErrorType>(parts[2], out var type) == false) 
            {
                throw new ArgumentException("invalid serialised format");
            }

            return new Error(parts[0], parts[1], type);
        }

        public ErrorList ToErrorList()
        {
            return new([this]);
        }
    }

    public enum ErrorType
    {
        Validation,
        NotFound,
        Failure,
        Conflict
    }
}
