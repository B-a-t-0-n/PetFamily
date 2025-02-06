using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Species.Requests;
using PetFamily.API.Extensions;
using PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.AddBreed;
using PetFamily.Application.SpeciesManagment.Commands.SpeciesHandlers.Create;

namespace PetFamily.API.Controllers.Species
{
    public class SpeciesController : ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateSpeciesHandler handler,
            [FromBody] CreateSpeciesRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand();

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPost("{id:guid}/add-breed")]
        public async Task<ActionResult<Guid>> AddBreed(
            [FromRoute] Guid id,
            [FromServices] AddBreedHandler handler,
            [FromBody] AddBreedRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
    }
}
