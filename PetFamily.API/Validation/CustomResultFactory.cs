using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using PetFamily.Domain.Shared;
using PetFamily.API.Response;

namespace PetFamily.API.Validation
{
    public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
    {
        public IActionResult CreateActionResult(
            ActionExecutingContext context,
            ValidationProblemDetails? validationProblemDetails)
        {
            if (validationProblemDetails == null)
            {
                throw new InvalidOperationException("ValidationProblemDetails is null");
            }

            List<ResponseError> errors = [];

            foreach (var (invalidField, validationErrors) in validationProblemDetails.Errors)
            {
                var responceErrors = from errorMessage in validationErrors
                                     let error = Error.Deserialize(errorMessage)
                                     select new ResponseError(error.Code, error.Message, invalidField);

                errors.AddRange(responceErrors);
            }

            var envelope = Envelope.Error(errors);

            return new ObjectResult(envelope)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }
}
