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
using FluentValidation;
using PetFamily.Application.Extentions;
using PetFamily.Application.Messaging;
using System.Diagnostics;

namespace PetFamily.Application.Volunteers.AddPetPtotos
{
    public class AddPetPhotosHandler
    {
        private const string BUCKET_NAME = "photos";

        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IFileProvider _fileProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddPetPhotosCommand> _validator;
        private readonly ILogger<AddPetPhotosHandler> _logger;
        private readonly IMessageQueue<IEnumerable<FileMetadata>> _messageQueue;

        public AddPetPhotosHandler(
            IVolunteerRepository volunteerRepository,
            IUnitOfWork unitOfWork,
            ILogger<AddPetPhotosHandler> logger,
            IFileProvider fileProvider,
            IValidator<AddPetPhotosCommand> validator,
            IMessageQueue<IEnumerable<FileMetadata>> messageQueue)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _fileProvider = fileProvider;
            _validator = validator;
            _messageQueue = messageQueue;
        }

        public async Task<Result<IReadOnlyList<PhotoPath>, ErrorList>> Handle(AddPetPhotosCommand command, CancellationToken cancellationToken = default)
        {
            var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

            try
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                if (validationResult.IsValid == false)
                {
                    return validationResult.ToErrorList();
                }

                var volunteerResult = await _volunteerRepository.GetById(
                    VolunteerId.Create(command.VolunteerId), cancellationToken);

                if (volunteerResult.IsFailure)
                    return volunteerResult.Error.ToErrorList();

                var petId = PetId.Create(command.PetId);

                var pet = volunteerResult.Value.Pets.FirstOrDefault(p => p.Id == petId);
                if (pet is null)
                    return Errors.General.NotFound(petId).ToErrorList();

                List<FileData> filesData = [];
                foreach (var file in command.Files)
                {
                    var extension = Path.GetExtension(file.FileName);

                    var photoPathResult = PhotoPath.Create(Guid.NewGuid().ToString(), extension);
                    if (photoPathResult.IsFailure)
                        return photoPathResult.Error.ToErrorList();

                    var fileContent = new FileData(file.Content, photoPathResult.Value, BUCKET_NAME);

                    var petPhotoId = PetPhotoId.NewPetPhotoId();

                    var photoResult = PetPhoto.Create(petPhotoId, photoPathResult.Value, false);
                    if (photoResult.IsFailure)
                        return photoResult.Error.ToErrorList();

                    pet.AddPetPhoto(photoResult.Value);

                    filesData.Add(fileContent);
                }

                await _unitOfWork.SaveChanges(cancellationToken);

                var uploadResult = await _fileProvider.UploadFiles(filesData, cancellationToken);
                if (uploadResult.IsFailure)
                {
                    await _messageQueue.WriteAsync(
                        filesData.Select(f => new FileMetadata(f.BucketName, f.FilePath.PathToStorage)),
                        cancellationToken);

                    return uploadResult.Error.ToErrorList();
                }

                transaction.Commit();

                return uploadResult.Value.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Can not add pet photos to pet - {id} in transaction", command.PetId);

                transaction.Rollback();
                return Error.Failure("Can not add pet photos to pet - {id}", "pet.petPhotos.failure").ToErrorList();
            }
        }
    }
}
