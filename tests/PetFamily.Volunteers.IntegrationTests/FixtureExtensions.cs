using AutoFixture;
using PetFamily.Core.Dtos;
using PetFamily.Volunteers.Application.Commands.PetHandlers.AddPet.Commands;
using PetFamily.Volunteers.Application.Commands.PetHandlers.AddPetPhotos.Commands;
using PetFamily.Volunteers.Application.Commands.PetHandlers.DeletePetPhoto.Commands;
using PetFamily.Volunteers.Application.Commands.PetHandlers.HardDeletePet.Commands;
using PetFamily.Volunteers.Application.Commands.PetHandlers.MovePet.Commands;
using PetFamily.Volunteers.Application.Commands.PetHandlers.SetMainPhotoPet.Commands;
using PetFamily.Volunteers.Application.Commands.PetHandlers.SoftDeletePet.Commands;
using PetFamily.Volunteers.Application.Commands.PetHandlers.UpdateInfoPet.Commands;
using PetFamily.Volunteers.Application.Commands.PetHandlers.UpdatePetStatus.Commands;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.Create.Commands;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.HardDelete.Commands;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.SoftDelete.Commands;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateDetailsForAssistance.Commands;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateMainInfo.Commands;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateSocialNetwork.Commands;
using System.Text;

namespace PetFamily.Volunteers.IntegrationTests;

public static class FixtureExtensions
{
    public static CreateVolunteerCommand CreateVolunteerCommand(this Fixture fixture)
    {
        return fixture.Build<CreateVolunteerCommand>()
                      .With(v => v.FullName,
                            new FullNameDto(
                                "testname",
                                "testlastname",
                                "testpatronymic"))
                      .With(c => c.PhoneNumber, "89123456789")
                      .Create();
    }

    public static HardDeleteVolunteerCommand HardDeleteVolunteerCommand(
        this Fixture fixture,
        Guid volunteerId)
    {
        return fixture.Build<HardDeleteVolunteerCommand>()
                      .With(v => v.Id, volunteerId)
                      .Create();
    }

    public static SoftDeleteVolunteerCommand SoftDeleteVolunteerCommand(
        this Fixture fixture,
        Guid volunteerId)
    {
        return fixture.Build<SoftDeleteVolunteerCommand>()
                      .With(v => v.Id, volunteerId)
                      .Create();
    }

    public static UpdateDetailsForAssistanceCommand UpdateDetailsForAssistanceCommand(
        this Fixture fixture,
        Guid volunteerId)
    {
        return fixture.Build<UpdateDetailsForAssistanceCommand>()
                      .With(v => v.Id, volunteerId)
                      .With(v => v.DetailsForAssistance,
                            new List<DetailsForAssistanceDto>
                            {
                                new DetailsForAssistanceDto()
                                {
                                    Name = "testname",
                                    Description = "testdescription"
                                }
                            })
                      .Create();
    }

    public static UpdateMainInfoCommand UpdateMainInfoCommand(
        this Fixture fixture,
        Guid volunteerId)
    {
        return fixture.Build<UpdateMainInfoCommand>()
                      .With(v => v.Id, volunteerId)
                      .With(v => v.FullName,
                            new FullNameDto(
                                "testname",
                                "testlastname",
                                "testpatronymic"))
                      .With(v => v.PhoneNumber, "89123456789")
                      .Create();
    }

    public static UpdateSocialNetworkCommand UpdateSocialNetworkCommand(
        this Fixture fixture,
        Guid volunteerId)
    {
        return fixture.Build<UpdateSocialNetworkCommand>()
                      .With(v => v.Id, volunteerId)
                      .With(v => v.SocialNetwork,
                            new List<SocialNetworkDto>
                            {
                                new SocialNetworkDto()
                                {
                                    Name = "testname",
                                    Link = "testurl"
                                }
                            })
                      .Create();
    }

    public static AddPetCommand AddPetCommand(
        this Fixture fixture,
        Guid volunteerId,
        Guid speciesId,
        Guid breedId)
    {
        return fixture.Build<AddPetCommand>()
                      .With(v => v.VolunteerId, volunteerId)
                      .With(v => v.Nickname, "testnickname")
                      .With(v => v.Description, "testdescription")
                      .With(v => v.Color, "testcolor")
                      .With(v => v.HealthInformation, "testhealthinformation")
                      .With(v => v.Size, new SizeDto(1, 1))
                      .With(v => v.PhoneNumber, "89123456789")
                      .With(v => v.AssistanceStatus, "needshelp")
                      .With(v => v.DateOfBirth, DateTime.UtcNow)
                      .With(v => v.DetailsForAssistance,
                            new List<DetailsForAssistanceDto>
                            {
                                new DetailsForAssistanceDto()
                                {
                                    Name = "testname",
                                    Description = "testdescription"
                                }
                            })
                      .With(v => v.Address,
                            new AddressDto(
                                "testcity",
                                "teststreet",
                                "testhouse",
                                "testflat",
                                "testapartmentnumber"))
                      .With(v => v.SpeciesAndBreed,
                            new SpeciesAndBreedDto(speciesId, breedId))
                      .Create();
    }

    public static HardDeletePetCommand HardDeletePetCommand(
        this Fixture fixture,
        Guid volunteerId,
        Guid petId)
    {
        return fixture.Build<HardDeletePetCommand>()
                      .With(v => v.VolunteerId, volunteerId)
                      .With(v => v.PetId, petId)
                      .Create();
    }

    public static SoftDeletePetCommand SoftDeletePetCommand(
        this Fixture fixture,
        Guid volunteerId,
        Guid petId)
    {
        return fixture.Build<SoftDeletePetCommand>()
                      .With(v => v.VolunteerId, volunteerId)
                      .With(v => v.PetId, petId)
                      .Create();
    }

    public static MovePetCommand MovePetCommand(
        this Fixture fixture,
        Guid volunteerId,
        Guid petId,
        int position)
    {
        return fixture.Build<MovePetCommand>()
                      .With(v => v.VolunteerId, volunteerId)
                      .With(v => v.PetId, petId)
                      .With(v => v.SerialNumber, position)
                      .Create();
    }

    public static UpdatePetInfoCommand UpdatePetInfoCommand(
        this Fixture fixture,
        Guid volunteerId,
        Guid petId)
    {
        return fixture.Build<UpdatePetInfoCommand>()
                      .With(v => v.VolunteerId, volunteerId)
                      .With(v => v.PetId, petId)
                      .With(v => v.Nickname, "testnickname")
                      .With(v => v.Description, "testdescription")
                      .With(v => v.Color, "testcolor")
                      .With(v => v.HealthInformation, "testhealthinformation")
                      .With(v => v.Size, new SizeDto(1, 1))
                      .With(v => v.PhoneNumber, "89123456789")
                      .With(v => v.AssistanceStatus, "needshelp")
                      .With(v => v.DateOfBirth, DateTime.UtcNow)
                      .With(v => v.DetailsForAssistance,
                            new List<DetailsForAssistanceDto>
                            {
                                new DetailsForAssistanceDto()
                                {
                                    Name = "testname",
                                    Description = "testdescription"
                                }
                            })
                      .With(v => v.Address,
                            new AddressDto(
                                "testcity",
                                "teststreet",
                                "testhouse",
                                "testflat",
                                "testapartmentnumber"))
                      .Create();
    }

    public static UpdatePetStatusCommand UpdatePetStatusCommand(
        this Fixture fixture,
        Guid volunteerId,
        Guid petId,
        string status)
    {
        return fixture.Build<UpdatePetStatusCommand>()
                      .With(v => v.VolunteerId, volunteerId)
                      .With(v => v.PetId, petId)
                      .With(v => v.AssistanceStatus, status)
                      .Create();
    }

    public static DeletePetPhotoCommand DeletePetPhotoCommand(
        this Fixture fixture,
        Guid volunteerId,
        Guid petId,
        Guid photoId)
    {
        return fixture.Build<DeletePetPhotoCommand>()
                      .With(v => v.VolunteerId, volunteerId)
                      .With(v => v.PetId, petId)
                      .With(v => v.PetPhotoId, photoId)
                      .Create();
    }

    public static SetMainPhotoPetCommand SetMainPhotoPetCommand(
        this Fixture fixture,
        Guid volunteerId,
        Guid petId,
        string path)
    {
        return fixture.Build<SetMainPhotoPetCommand>()
                      .With(v => v.VolunteerId, volunteerId)
                      .With(v => v.PetId, petId)
                      .With(v => v.Path, path)
                      .Create();
    }
}
