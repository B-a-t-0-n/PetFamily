using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;

namespace PetFamily.API.Controllers
{
    public class FileController : ApplicationController
    {
        [HttpPost]
        public async Task<IActionResult> CreateFile(
            IFormFile file,
            [FromServices]IFileProvider fileProvider,
            CancellationToken cancellationToken = default)
        {
            await using var stream = file.OpenReadStream();

            var fileData = new FileData(stream, "photos", "");

            var result = await fileProvider.Uploadfile(fileData, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteFile(
            [FromRoute] Guid id,
            [FromServices] IFileProvider fileProvider,
            CancellationToken cancellationToken = default)
        {
            var fileMetadata = new FileMetadata("photos", id.ToString());

            var result = await fileProvider.Deletefile(fileMetadata, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetFile(
            [FromRoute] Guid id,
            [FromServices] IFileProvider fileProvider,
            CancellationToken cancellationToken = default)
        {
            var fileMetadata = new FileMetadata("photos", id.ToString());

            var result = await fileProvider.GetfileURL(fileMetadata, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
    }
}
