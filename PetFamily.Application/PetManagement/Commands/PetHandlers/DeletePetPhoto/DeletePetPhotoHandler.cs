using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Extentions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.DeletePetPhoto.Commands;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;

namespace PetFamily.Application.PetManagement.UseCases.PetHandlers.DeletePetPhoto
{
    public class DeletePetPhotoHandler : ICommandHandler<Guid, DeletePetPhotoCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<DeletePetPhotoHandler> _logger;
        private readonly IValidator<DeletePetPhotoCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePetPhotoHandler(
            IVolunteerRepository volunteerRepository,
            IFileProvider fileProvider,
            ILogger<DeletePetPhotoHandler> logger,
            IValidator<DeletePetPhotoCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _volunteerRepository = volunteerRepository;
            _fileProvider = fileProvider;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(DeletePetPhotoCommand command, CancellationToken cancellationToken = default)
        {
            var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

            try
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                if (validationResult.IsValid == false)
                {
                    return validationResult.ToErrorList();
                }

                var volunteerId = VolunteerId.Create(command.VolunteerId);

                var volunteerResult = await _volunteerRepository.GetById(volunteerId);
                if (volunteerResult.IsFailure)
                    return volunteerResult.Error.ToErrorList();

                var pathToStorageResult = volunteerResult.Value.DeletePetPhoto(
                    PetId.Create(command.PetId),
                    PetPhotoId.Create(command.PetPhotoId));
                if (pathToStorageResult.IsFailure)
                    return pathToStorageResult.Error.ToErrorList();

                await _unitOfWork.SaveChanges(cancellationToken);

                var fileMetadata = new FileMetadata(Constants.PHOTO_BUCKET_NAME, pathToStorageResult.Value);

                var deleteResult = await _fileProvider.Deletefile(fileMetadata, cancellationToken);

                if (deleteResult.IsFailure)
                    return deleteResult.Error.ToErrorList();

                transaction.Commit();

                _logger.LogInformation("deleted pet photo with id {id}",
                    command.PetPhotoId.ToString()
                    );

                return volunteerId.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Can not delete pet photo to pet - {id} in transaction", command.PetId);

                transaction.Rollback();
                return Error.Failure("Can not delete pet photo to pet - {id}", "pet.petPhoto.failure").ToErrorList();
            }
        }
    }
}
