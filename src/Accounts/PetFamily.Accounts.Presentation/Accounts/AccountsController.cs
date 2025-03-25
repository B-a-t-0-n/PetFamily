using Microsoft.AspNetCore.Mvc;
using PetFamily.Accounts.Application.Commands.Login;
using PetFamily.Accounts.Application.Commands.RefreshTokens;
using PetFamily.Accounts.Application.Commands.Register;
using PetFamily.Accounts.Contracts.Responses;
using PetFamily.Accounts.Presentation.Accounts.Requests;
using PetFamily.Framework;

namespace PetFamily.Accounts.Presentation.Accounts;

public class AccountsController : ApplicationController
{
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

        return Ok(result.Value);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> Refresh(
        [FromBody] RefreshTokensRequest request,
        [FromServices] RefreshTokensHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}