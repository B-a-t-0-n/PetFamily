using Microsoft.AspNetCore.Mvc;
using PetFamily.Accounts.Application.Commands.Login;
using PetFamily.Accounts.Application.Commands.RefreshTokens;
using PetFamily.Accounts.Application.Commands.RefreshTokens.Command;
using PetFamily.Accounts.Application.Commands.Register;
using PetFamily.Accounts.Application.Queries;
using PetFamily.Accounts.Contracts.Responses;
using PetFamily.Accounts.Presentation.Accounts.Requests;
using PetFamily.Framework;
using PetFamily.SharedKernel;
using System.Threading;

namespace PetFamily.Accounts.Presentation.Accounts;

public class AccountsController : ApplicationController
{
    private const string REFRESH_TOKEN = "refreshToken";

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetUser(
        [FromRoute] Guid id,
        [FromServices] GetUserByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetUserByIdQuery(id);

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("registration")]
    public async Task<ActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        HttpContext.Response.Cookies.Append(REFRESH_TOKEN, result.Value.RefreshToken.ToString());

        return Ok(result.Value);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<LoginResponse>> Refresh(
        [FromBody] RefreshTokensRequest request,
        [FromServices] RefreshTokensHandler handler,
        CancellationToken cancellationToken = default)
    {
        if (HttpContext is null ||
            !HttpContext.Request.Cookies.TryGetValue(REFRESH_TOKEN, out var refreshToken))
            return Unauthorized();

        var command = request.ToCommand(Guid.Parse(refreshToken));
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        HttpContext.Response.Cookies.Append(REFRESH_TOKEN, result.Value.RefreshToken.ToString());

        return Ok(result.Value);
    }
}
