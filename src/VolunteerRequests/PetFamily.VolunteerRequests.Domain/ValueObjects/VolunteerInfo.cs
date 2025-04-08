using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.VolunteerRequests.Domain.ValueObjects;

public class VolunteerInfo : ValueObject
{
    private VolunteerInfo() { }
    private VolunteerInfo(
        Description description,
        PhoneNumber phoneNumber,
        YearsExperience yearsExperience,
        IEnumerable<Requisites> requisites,
        IEnumerable<string> certificates)
    {
        Description = description;
        PhoneNumber = phoneNumber;
        YearsExperience = yearsExperience;
        Requisites = requisites;
        Certificates = certificates;
    }

    public Description Description { get; } = default!;

    public PhoneNumber PhoneNumber { get; } = default!;

    public YearsExperience YearsExperience { get; } = default!;

    public IEnumerable<Requisites> Requisites { get; } = [];

    public IEnumerable<string> Certificates { get; } = [];

    public static Result<VolunteerInfo, Error> Create(
        Description description,
        PhoneNumber phoneNumber,
        YearsExperience yearsExperience,
        IEnumerable<Requisites> requisites,
        IEnumerable<string> certificates)
    {
        if (yearsExperience == null)
            return Errors.General.ValueIsInvalid("yearsExperience");
        if (phoneNumber == null)
            return Errors.General.ValueIsInvalid("phoneNumber");
        if (description == null)
            return Errors.General.ValueIsInvalid("description");

        var volunteerInfo = new VolunteerInfo(description, phoneNumber, yearsExperience, requisites, certificates);

        return volunteerInfo;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Description.Value == null ? "" : Description.Value;
        yield return PhoneNumber.Number == null ? "" : PhoneNumber.Number;
        yield return YearsExperience.Value;

        foreach (var requisites in Requisites)
        {
            yield return requisites.Name == null ? "" : requisites.Name;
            yield return requisites.Description == null ? "" : requisites.Description;
        }

        foreach (var certificate in Certificates)
        {
            yield return certificate == null ? "" : certificate;
        }
    }
}