using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Core.Dtos;
using PetFamily.Core.Models;
using PetFamily.Framework;
using PetFamily.Volunteers.Application.Commands.PetHandlers.AddPet;
using PetFamily.Volunteers.Application.Commands.PetHandlers.AddPetPhotos;
using PetFamily.Volunteers.Application.Commands.PetHandlers.AddPetPhotos.Commands;
using PetFamily.Volunteers.Application.Commands.PetHandlers.DeletePetPhoto;
using PetFamily.Volunteers.Application.Commands.PetHandlers.DeletePetPhoto.Commands;
using PetFamily.Volunteers.Application.Commands.PetHandlers.HardDeletePet;
using PetFamily.Volunteers.Application.Commands.PetHandlers.HardDeletePet.Commands;
using PetFamily.Volunteers.Application.Commands.PetHandlers.MovePet;
using PetFamily.Volunteers.Application.Commands.PetHandlers.SetMainPhotoPet;
using PetFamily.Volunteers.Application.Commands.PetHandlers.SoftDeletePet;
using PetFamily.Volunteers.Application.Commands.PetHandlers.SoftDeletePet.Commands;
using PetFamily.Volunteers.Application.Commands.PetHandlers.UpdateInfoPet;
using PetFamily.Volunteers.Application.Commands.PetHandlers.UpdatePetStatus;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.Create;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.HardDelete;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.HardDelete.Commands;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.SoftDelete;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.SoftDelete.Commands;
using PetFamily.Volunteers.Application.Commands.VolunteersHandlers.UpdateMainInfo;
using PetFamily.Volunteers.Application.Queries.VolunteerHandlers.GetVolunteerById;
using PetFamily.Volunteers.Application.Queries.VolunteerHandlers.GetVolunteersWithPagination;
using PetFamily.Volunteers.Presentation.Processors;
using PetFamily.Volunteers.Presentation.Volunteer.Requests;

namespace PetFamily.Volunteers.Presentation.Volunteer;

public class VolunteerController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult<PagedList<VolunteerDto>>> Get(
        [FromQuery] GetVolunteersWithPaginationRequest request,
        [FromServices] GetVolunteersWithPaginationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VolunteerDto>> Get(
        [FromRoute] Guid id,
        [FromServices] GetVolunteerByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetVolunteerByIdQuery(id);

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult<Guid>> Update(
        [FromRoute] Guid id,
        [FromServices] UpdateMainInfoHandler handler,
        [FromBody] UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete("{id:guid}/soft")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] SoftDeleteVolunteerHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new SoftDeleteVolunteerCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete("{id:guid}/hard")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] HardDeleteVolunteerHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new HardDeleteVolunteerCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [Authorize]
    [HttpPost("{id:guid}/add-pet")]
    public async Task<ActionResult<Guid>> AddPet(
        [FromRoute] Guid id,
        [FromServices] AddPetHandler handler,
        [FromBody] AddPetRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPut("{volunteerId:guid}/{petId:guid}/info")]
    public async Task<ActionResult<Guid>> UpdatePetInfo(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] UpdatePetInfoHandler handler,
        [FromBody] UpdatePetInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(volunteerId, petId);
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        return Ok(result.Value);
    }

    [Authorize]
    [HttpPut("{volunteerId:guid}/{petId:guid}/assistance-status")]
    public async Task<ActionResult<Guid>> UpdatePetStatus(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] UpdatePetStatusHandler handler,
        [FromBody] UpdatePetStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(volunteerId, petId);
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete("{volunteerId:guid}/{petId:guid}/soft")]
    public async Task<ActionResult<Guid>> DeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] SoftDeletePetHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new SoftDeletePetCommand(volunteerId, petId);
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete("{volunteerId:guid}/{petId:guid}/hard")]
    public async Task<ActionResult<Guid>> DeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] HardDeletePetHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new HardDeletePetCommand(volunteerId, petId);
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        return Ok();
    }

    [Authorize]
    [HttpPost("{volunteerId:guid}/{petId:guid}/add-pet-photos")]
    public async Task<ActionResult<Guid>> AddPetPhotos(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromForm]IFormFileCollection files,
        [FromServices] AddPetPhotosHandler handler,
        CancellationToken cancellationToken = default)
    {
        await using var fileProcessor = new FormFileProcessor();
        var fileDtos = fileProcessor.Process(files);

        var command = new AddPetPhotosCommand(volunteerId, petId, fileDtos);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete("{volunteerId:guid}/{petId:guid}/delete-pet-photo/{photoId:guid}")]
    public async Task<ActionResult<Guid>> DeletePetPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromRoute] Guid photoId,
        [FromServices] DeletePetPhotoHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeletePetPhotoCommand(volunteerId, petId, photoId);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [Authorize]
    [HttpPost("{volunteerId:guid}/{petId:guid}/move-pet")]
    public async Task<ActionResult<Guid>> MovePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] MovePetHandler handler,
        [FromBody] MovePetRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(volunteerId, petId);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPost("{volunteerId:guid}/{petId:guid}/main-photo")]
    public async Task<ActionResult<Guid>> SetMainPhotoPet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] SetMainPhotoPetHandler handler,
        [FromBody] SetMainPhotoPetRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(volunteerId, petId);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }
}
