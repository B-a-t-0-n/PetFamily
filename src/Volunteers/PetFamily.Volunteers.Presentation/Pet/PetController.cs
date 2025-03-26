using Microsoft.AspNetCore.Mvc;
using PetFamily.Core.Dtos;
using PetFamily.Core.Models;
using PetFamily.Framework;
using PetFamily.Volunteers.Application.Queries.PetHandlers.GetPetById;
using PetFamily.Volunteers.Application.Queries.PetHandlers.GetPetWithPaginationFiltration;
using PetFamily.Volunteers.Presentation.Pet.Requests;

namespace PetFamily.Volunteers.Presentation.Pet;

public class PetController : ApplicationController
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PetDto>> Get(
        [FromRoute] Guid id,
        [FromServices] GetPetByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPetByIdQuery(id);

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<PetDto>>> Get(
        [FromQuery] GetPetWithPaginationFiltrationRequest request,
        [FromServices] GetPetWithPaginationFiltrationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response);
    }
}
