using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.Create.Commands;
using PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Dtos;
using PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Commands;
using PetFamily.Application.Volunteers.UpdateDetailsForAssistance;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateMainInfo.Dtos;
using PetFamily.Application.Volunteers.UpdateMainInfo.Commands;
using PetFamily.Application.Volunteers.UpdateSocialNetwork;
using PetFamily.Application.Volunteers.UpdateSocialNetwork.Dtos;
using PetFamily.Application.Volunteers.UpdateSocialNetwork.Commands;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.Delete.Commands;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.AddPet.Commands;
using PetFamily.Application.Volunteers.AddPet.Dtos;

namespace PetFamily.API.Controllers.Volunteer
{
    public class VolunteerController : ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerCommand request,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult<Guid>> Update(
            [FromRoute] Guid id,
            [FromServices] UpdateMainInfoHandler handler,
            [FromBody] UpdateMainInfoDto dto,
            [FromServices] IValidator<UpdateMainInfoCommand> validator,
            CancellationToken cancellationToken = default)
        {
            var command = new UpdateMainInfoCommand(id, dto);

            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToValidationErrorResponse();

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/social-network")]
        public async Task<ActionResult<Guid>> Update(
            [FromRoute] Guid id,
            [FromServices] UpdateSocialNetworkHandler handler,
            [FromBody] UpdateSocialNetworkDto dto,
            [FromServices] IValidator<UpdateSocialNetworkCommand> validator,
            CancellationToken cancellationToken = default)
        {
            var command = new UpdateSocialNetworkCommand(id, dto);

            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToValidationErrorResponse();

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/details-for-assistance")]
        public async Task<ActionResult<Guid>> Update(
            [FromRoute] Guid id,
            [FromServices] UpdateDetailsForAssistanceHandler handler,
            [FromBody] UpdateDetailsForAssistanceDto dto,
            [FromServices] IValidator<UpdateDetailsForAssistanceCommand> validator,
            CancellationToken cancellationToken = default)
        {
            var command = new UpdateDetailsForAssistanceCommand(id, dto);

            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToValidationErrorResponse();

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteVolunteerHandler handler,
            [FromServices] IValidator<DeleteVolunteerCommand> validator,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteVolunteerCommand(id);

            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToValidationErrorResponse();

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPost("{id:guid}/add-pet")]
        public async Task<ActionResult<Guid>> AddPet(
            [FromRoute] Guid id,
            [FromServices] AddPetHandler handler,
            [FromBody] PetDto dto,
            [FromServices] IValidator<AddPetCommand> validator,
            CancellationToken cancellationToken = default)
        {
            var command = new AddPetCommand(id, dto);

            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToValidationErrorResponse();

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
    }
}
