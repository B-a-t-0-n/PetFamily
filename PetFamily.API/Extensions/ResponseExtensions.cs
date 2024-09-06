using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Response;
using PetFamily.Domain.Shared;
using System.Diagnostics;

namespace PetFamily.API.Extensions
{
    public static class ResponseExtensions
    {
        public static ActionResult ToResponse(this Error error)
        {
            var statusCode = GetStatusCodeForErrorType(error.Type);

            var envelope = Envelope.Error(error);

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode,
            };
        }

        private static int GetStatusCodeForErrorType(ErrorType error)
        {
            return error switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
