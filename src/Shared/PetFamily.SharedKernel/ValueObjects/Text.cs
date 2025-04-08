using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects;

public class Text : ValueObject
{
    private Text() { }
    private Text(string? value)
    {
        Value = value;
    }

    public string? Value { get; } = default!;

    public static Result<Text, Error> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("text");

        if (value.Length > Constants.MAX_HIGHT_TEXT_LENGTH)
            return Errors.General.ValueIsRequired("text");

        var text = new Text(value);

        return text;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value == null ? "" : Value;
    }
}