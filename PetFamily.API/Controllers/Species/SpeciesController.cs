using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Species.Requests;
using PetFamily.API.Controllers.Volunteer.Requests;
using PetFamily.API.Extensions;
using PetFamily.Application.PetManagement.Queries.GetVolunteersWithPagination;
using PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.AddBreed;
using PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.RemoveBreed;
using PetFamily.Application.SpeciesManagment.Commands.BreedHandlers.RemoveBreed.Commands;
using PetFamily.Application.SpeciesManagment.Commands.SpeciesHandlers.Create;
using PetFamily.Application.SpeciesManagment.Commands.SpeciesHandlers.Delete;
using PetFamily.Application.SpeciesManagment.Commands.SpeciesHandlers.Delete.Commands;
using PetFamily.Application.SpeciesManagment.Queries.BreedQuery.GetBreedWithPagination;
using PetFamily.Application.SpeciesManagment.Queries.SpeciesQuery.GetSpeciesWithPagination;

namespace PetFamily.API.Controllers.Species
{
    public class SpeciesController : ApplicationController
    {
        [HttpGet]
        public async Task<ActionResult> Get(
            [FromQuery] GetSpeciesWithPaginationRequest request,
            [FromServices] GetSpeciesWithPaginationHandler handler,
            CancellationToken cancellationToken = default)
        {
            var query = request.ToQuery();

            var response = await handler.Handle(query, cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id:guid}/breeds")]
        public async Task<ActionResult> Get(
            [FromRoute] Guid id,
            [FromQuery] GetBreedWithPaginationRequest request,
            [FromServices] GetBreedWithPaginationHandler handler,
            CancellationToken cancellationToken = default)
        {
            var query = request.ToQuery(id);

            var response = await handler.Handle(query, cancellationToken);

            return Ok(response);
        }

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

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteSpeciesHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteSpeciesCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [HttpDelete("{speciesId:guid}/remove-breed/{breedId:guid}")]
        public async Task<ActionResult<Guid>> RemoveBreed(
            [FromRoute] Guid speciesId,
            [FromRoute] Guid breedId,
            [FromServices] RemoveBreedHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new RemoveBreedCommand(speciesId, breedId);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }
    }
}
