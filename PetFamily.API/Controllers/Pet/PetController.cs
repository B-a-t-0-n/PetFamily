using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Pet.Requests;
using PetFamily.Application.PetManagement.Queries.PetHandlers.GetPetById;
using PetFamily.Application.PetManagement.Queries.PetHandlers.GetPetWithPaginationFiltration;

namespace PetFamily.API.Controllers.Pet
{
    public class PetController : ApplicationController
    {
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> Get(
            [FromRoute] Guid id,
            [FromServices] GetPetByIdHandler handler,
            CancellationToken cancellationToken = default)
        {
            var query = new GetPetByIdQuery(id);

            var response = await handler.Handle(query, cancellationToken);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> Get(
            [FromQuery] GetPetWithPaginationFiltrationRequest request,
            [FromServices] GetPetWithPaginationFiltrationHandler handler,
            CancellationToken cancellationToken = default)
        {
            var query = request.ToQuery();

            var response = await handler.Handle(query, cancellationToken);

            return Ok(response);
        }
    }
}
