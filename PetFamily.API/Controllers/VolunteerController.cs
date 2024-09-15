using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.Create.Requests;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateMainInfo.Dtos;
using PetFamily.Application.Volunteers.UpdateMainInfo.Requests;
using PetFamily.Application.Volunteers.UpdateSocialNetwork;
using PetFamily.Application.Volunteers.UpdateSocialNetwork.Dtos;
using PetFamily.Application.Volunteers.UpdateSocialNetwork.Requests;

namespace PetFamily.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteerController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.Handle(request, cancellationToken);

            if(result.IsFailure)
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
            if(validationResult.IsValid == false)
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
    }
}
