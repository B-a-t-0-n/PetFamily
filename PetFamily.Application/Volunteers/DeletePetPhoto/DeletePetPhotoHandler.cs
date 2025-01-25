using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.DeletePetPhoto.Commands;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Infrastucture.Repositories;

namespace PetFamily.Application.Volunteers.DeletePetPhoto
{
    public class DeletePetPhotoHandler
    {
        private const string BUCKET_NAME = "photos";

        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<DeletePetPhotoHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePetPhotoHandler(
            IVolunteerRepository volunteerRepository,
            IFileProvider fileProvider,
            ILogger<DeletePetPhotoHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _volunteerRepository = volunteerRepository;
            _fileProvider = fileProvider;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, Error>> Handle(DeletePetPhotoCommand command, CancellationToken cancellationToken = default)
        {
            var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

            try
            {
                var volunteerId = VolunteerId.Create(command.VolunteerId);

                var volunteerResult = await _volunteerRepository.GetById(volunteerId);
                if (volunteerResult.IsFailure)
                    return volunteerResult.Error;

                var pet = volunteerResult.Value.Pets.FirstOrDefault(p => p.Id == command.PetId);
                if (pet is null)
                    return Errors.General.NotFound(command.PetId);

                var photo = pet.PetPhotos.FirstOrDefault(p => p.Id == command.PetPhotoId);
                if (photo is null)
                    return Errors.General.NotFound(command.PetPhotoId);

                pet.DeletePetPhoto(photo);

                await _unitOfWork.SaveChanges(cancellationToken);

                var fileMetadata = new FileMetadata(BUCKET_NAME, photo.Path.PathToStorage);

                var deleteResult = await _fileProvider.Deletefile(fileMetadata, cancellationToken);

                if (deleteResult.IsFailure)
                    return deleteResult.Error;

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
                return Error.Failure("Can not delete pet photo to pet - {id}", "pet.petPhoto.failure");
            }
        }
    }
}
