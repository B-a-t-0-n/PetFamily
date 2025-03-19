using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Domain.ValueObjects;

public class Requisites : ValueObject
{
    public const int MAX_HIGHT_DESCRIPTION_LENGTH = 6000;

    private Requisites() { }
    private Requisites(string? name, string? descriptuon)
    {
        Name = name;
        Description = descriptuon;
    }

    public string? Name { get; } = default!;
    public string? Description { get; } = default!;

    public static Result<Requisites, Error> Create(string? name, string? description)
    {
        if (name != null && name.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsRequired("name");

        if (description != null && description.Length > MAX_HIGHT_DESCRIPTION_LENGTH)
            return Errors.General.ValueIsRequired("description");

        var detailsForAssistance = new Requisites(name, description);

        return detailsForAssistance;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Name == null ? "" : Name; ;
        yield return Description == null ? "" : Description; ;
    }
}
