using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.IDs;
using PetFamily.Volunteers.Domain.ValueObjects;
using Color = PetFamily.Volunteers.Domain.ValueObjects.Color;
using Size = PetFamily.Volunteers.Domain.ValueObjects.Size;

namespace PetFamily.Volunteers.Domain.Entity;

public class Pet : SoftDeletableEntity<PetId>
{
    private readonly List<PetPhoto> _petPhotos = [];
    private List<DetailsForAssistance> _detailsForAssistance = [];

    //ef core
    private Pet(PetId id) : base(id) { }

    private Pet(
        PetId id,
        Nickname nickname,
        SpeciesAndBreed speciesAndBreed,
        Description description,
        Color color,
        HealthInformation healthInformation,
        Address address,
        Size size,
        PhoneNumber phoneNumber,
        bool isCastrated,
        DateTime? dateOfBirth,
        bool isVaccinated,
        AssistanceStatus assistanceStatus,
        DateTime dateOfCreation,
        List<DetailsForAssistance> detailsForAssistance
        ) : base(id)
    {
        Nickname = nickname;
        Description = description;
        Color = color;
        HealthInformation = healthInformation;
        SpeciesAndBreed = speciesAndBreed;
        Address = address;
        Size = size;
        PhoneNumber = phoneNumber;
        IsCastrated = isCastrated;
        DateOfBirth = dateOfBirth;
        IsVaccinated = isVaccinated;
        AssistanceStatus = assistanceStatus;
        DateOfCreation = dateOfCreation;
        _detailsForAssistance = detailsForAssistance;
    }

    public Nickname Nickname { get; private set; } = default!;

    public SpeciesAndBreed SpeciesAndBreed { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public Color Color { get; private set; } = default!;

    public SerialNumber SerialNumber { get; private set; } = default!;

    public HealthInformation HealthInformation { get; private set; } = default!;

    public Address Address { get; private set; } = default!;

    public Size Size { get; private set; } = default!;

    public PhoneNumber PhoneNumber { get; private set; } = default!;

    public bool IsCastrated { get; private set; }

    public DateTime? DateOfBirth { get; private set; }

    public bool IsVaccinated { get; private set; }

    public AssistanceStatus AssistanceStatus { get; private set; } = default!;

    public DateTime DateOfCreation { get; private set; }

    public IReadOnlyList<DetailsForAssistance> DetailsForAssistance => _detailsForAssistance;

    public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;

    internal void AddPhoto(PetPhoto petPhoto)
    {
        _petPhotos.Add(petPhoto);
    }

    internal UnitResult<Error> SetMainPhoto(PhotoPath petPhoto)
    {
        var oldMainPhoto = _petPhotos.FirstOrDefault(x => x.IsMain);
        if (oldMainPhoto is not null)
            oldMainPhoto.SetIsMain(false);

        var newMainPhoto = _petPhotos.FirstOrDefault(x => x.Path == petPhoto);
        if (newMainPhoto is null)
            return Errors.General.NotFound();

        newMainPhoto.SetIsMain(true);

        return Result.Success<Error>();
    }

    internal void DeletePhoto(PetPhoto petPhoto)
    {
        _petPhotos.Remove(petPhoto);
    }

    public static Result<Pet, Error> Create(
        PetId id,
        Nickname nickname,
        SpeciesAndBreed speciesAndBreed,
        Description description,
        Color color,
        HealthInformation healthInformation,
        Address address,
        Size size,
        PhoneNumber phoneNumber,
        bool isCastrated,
        DateTime? dateOfBirth,
        bool isVaccinated,
        AssistanceStatus assistanceStatus,
        DateTime dateOfCreation,
        List<DetailsForAssistance> detailsForAssistance)
    {
        var pet = new Pet(id,
            nickname,
            speciesAndBreed,
            description,
            color,
            healthInformation,
            address,
            size,
            phoneNumber,
            isCastrated,
            dateOfBirth,
            isVaccinated,
            assistanceStatus,
            dateOfCreation,
            detailsForAssistance);

        return pet;
    }

    internal void UpdateInfo(
        Nickname nickname,
        SpeciesAndBreed speciesAndBreed,
        Description description,
        Color color,
        HealthInformation healthInformation,
        Address address,
        Size size,
        PhoneNumber phoneNumber,
        bool isCastrated,
        DateTime? dateOfBirth,
        bool isVaccinated,
        AssistanceStatus assistanceStatus,
        List<DetailsForAssistance> detailsForAssistance)
    {
        Nickname = nickname;
        Description = description;
        Color = color;
        HealthInformation = healthInformation;
        SpeciesAndBreed = speciesAndBreed;
        Address = address;
        Size = size;
        PhoneNumber = phoneNumber;
        IsCastrated = isCastrated;
        DateOfBirth = dateOfBirth;
        IsVaccinated = isVaccinated;
        AssistanceStatus = assistanceStatus;
        _detailsForAssistance = detailsForAssistance;
    }

    internal void UpdateAssistanceStatus(AssistanceStatus assistanceStatus)
    {
        AssistanceStatus = assistanceStatus;
    }

    internal void SetSerialNumber(SerialNumber serialNumber) => SerialNumber = serialNumber;

    internal UnitResult<Error> MoveForward()
    {
        var newSerialNumber = SerialNumber.Forward();
        if (newSerialNumber.IsFailure)
            return newSerialNumber.Error;

        SerialNumber = newSerialNumber.Value;

        return Result.Success<Error>();
    }

    internal UnitResult<Error> MoveBack()
    {
        var newSerialNumber = SerialNumber.Back();
        if (newSerialNumber.IsFailure)
            return newSerialNumber.Error;

        SerialNumber = newSerialNumber.Value;

        return Result.Success<Error>();
    }

    internal void Move(SerialNumber newSerialNumber) => SerialNumber = newSerialNumber;

    internal bool IsExpired() => DeletionDate != null
                               && DateTime.UtcNow >= DeletionDate.Value
                                  .AddDays(Constants.LIFETIME_AFTER_DELETION);
}
