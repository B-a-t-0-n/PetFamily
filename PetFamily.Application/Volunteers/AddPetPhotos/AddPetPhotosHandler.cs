using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Infrastucture.Repositories;
using PetFamily.Application.Volunteers.AddPetPtotos.Commands;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Application.Providers;
using PetFamily.Domain.PetMenegment.Entity;
using PetFamily.Application.Database;
using System.Reflection;

namespace PetFamily.Application.Volunteers.AddPetPtotos
{
    public class AddPetPhotosHandler
    {
        private const string BUCKET_NAME = "photos";

        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IFileProvider _fileProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddPetPhotosHandler> _logger;

        public AddPetPhotosHandler(
            IVolunteerRepository volunteerRepository,
            IUnitOfWork unitOfWork,
            ILogger<AddPetPhotosHandler> logger,
            IFileProvider fileProvider)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _fileProvider = fileProvider;
        }

        public async Task<Result<IReadOnlyList<PhotoPath>, Error>> Handle(AddPetPhotosCommand command, CancellationToken cancellationToken = default)
        {
            var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

            try
            {
                var volunteerResult = await _volunteerRepository.GetById(
                    VolunteerId.Create(command.VolunteerId), cancellationToken);

                if (volunteerResult.IsFailure)
                    return volunteerResult.Error;

                var petId = PetId.Create(command.PetId);

                var pet = volunteerResult.Value.Pets.FirstOrDefault(p => p.Id == petId);
                if (pet is null)
                    return Errors.General.NotFound(petId);

                List<FileData> filesData = [];
                foreach (var file in command.Files)
                {
                    var extension = Path.GetExtension(file.FileName);

                    var photoPathResult = PhotoPath.Create(Guid.NewGuid().ToString(), extension);
                    if (photoPathResult.IsFailure)
                        return photoPathResult.Error;

                    var fileContent = new FileData(file.Content, photoPathResult.Value, BUCKET_NAME);

                    var petPhotoId = PetPhotoId.NewPetPhotoId();

                    var photoResult = PetPhoto.Create(petPhotoId, photoPathResult.Value, false);
                    if (photoResult.IsFailure)
                        return photoResult.Error;

                    pet.AddPetPhoto(photoResult.Value);

                    filesData.Add(fileContent);
                }

                await _unitOfWork.SaveChanges(cancellationToken);

                var uploadResult = await _fileProvider.UploadFiles(filesData, cancellationToken);

                if (uploadResult.IsFailure)
                    return uploadResult.Error;

                transaction.Commit();

                return uploadResult.Value.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Can not add pet photos to pet - {id} in transaction", command.PetId);

                transaction.Rollback();
                return Error.Failure("Can not add pet photos to pet - {id}", "pet.petPhotos.failure");
            }
        }
    }
}
