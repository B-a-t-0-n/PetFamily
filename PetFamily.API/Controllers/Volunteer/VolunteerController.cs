using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.API.Processors;
using PetFamily.API.Controllers.Volunteer.Requests;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateSocialNetwork;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Delete;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateDetailsForAssistance;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Create;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateMainInfo;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.AddPet;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.DeletePetPhoto;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.AddPetPhotos;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.MovePet;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Delete.Commands;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.AddPetPhotos.Commands;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.DeletePetPhoto.Commands;

namespace PetFamily.API.Controllers.Volunteer
{
    public class VolunteerController : ApplicationController
    {
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

        [HttpPut("{id:guid}/social-network")]
        public async Task<ActionResult<Guid>> Update(
            [FromRoute] Guid id,
            [FromServices] UpdateSocialNetworkHandler handler,
            [FromBody] UpdateSocialNetworkRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/details-for-assistance")]
        public async Task<ActionResult<Guid>> Update(
            [FromRoute] Guid id,
            [FromServices] UpdateDetailsForAssistanceHandler handler,
            [FromBody] UpdateDetailsForAssistanceRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteVolunteerHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteVolunteerCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

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
    }
}
