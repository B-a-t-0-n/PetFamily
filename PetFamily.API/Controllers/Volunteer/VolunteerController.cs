﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.Create.Requests;
using PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Dtos;
using PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Requests;
using PetFamily.Application.Volunteers.UpdateDetailsForAssistance;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateMainInfo.Dtos;
using PetFamily.Application.Volunteers.UpdateMainInfo.Requests;
using PetFamily.Application.Volunteers.UpdateSocialNetwork;
using PetFamily.Application.Volunteers.UpdateSocialNetwork.Dtos;
using PetFamily.Application.Volunteers.UpdateSocialNetwork.Requests;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.Delete.Requests;

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
            [FromServices] IValidator<UpdateMainInfoRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateMainInfoRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToValidationErrorResponse();

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/social-network")]
        public async Task<ActionResult<Guid>> Update(
            [FromRoute] Guid id,
            [FromServices] UpdateSocialNetworkHandler handler,
            [FromBody] UpdateSocialNetworkDto dto,
            [FromServices] IValidator<UpdateSocialNetworkRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateSocialNetworkRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToValidationErrorResponse();

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/details-for-assistance")]
        public async Task<ActionResult<Guid>> Update(
            [FromRoute] Guid id,
            [FromServices] UpdateDetailsForAssistanceHandler handler,
            [FromBody] UpdateDetailsForAssistanceDto dto,
            [FromServices] IValidator<UpdateDetailsForAssistanceRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateDetailsForAssistanceRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToValidationErrorResponse();

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteVolunteerHandler handler,
            [FromServices] IValidator<DeleteVolunteerRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new DeleteVolunteerRequest(id);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToValidationErrorResponse();

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
    }
}
