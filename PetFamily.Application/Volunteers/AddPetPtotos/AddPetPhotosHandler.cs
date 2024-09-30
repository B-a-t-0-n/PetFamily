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
using System.Diagnostics;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.Volunteers.AddPetPtotos
{
    public class AddPetPhotosHandler
    {
        private const string BUCKET_NAME = "photos";

        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<AddPetPhotosHandler> _logger;

        public AddPetPhotosHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<AddPetPhotosHandler> logger,
            IFileProvider fileProvider)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _fileProvider = fileProvider;
        }

        public async Task<Result<Guid, Error>> Handle(AddPetPhotosCommand command, CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteerRepository.GetById(
                VolunteerId.Create(command.VolunteerId), cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var petId = PetId.Create(command.PetId);

            var pet = volunteerResult.Value.Pets.FirstOrDefault(p => p.Id == petId);
            if (pet is null)
                return Errors.General.NotFound(petId);

            var files = new Dictionary<PhotoPath, FileContent>();

            foreach (var file in command.Files)
            {
                var extension = Path.GetExtension(file.FileName);

                var filePathResult = PhotoPath.Create(Guid.NewGuid().ToString(),extension);
                if(filePathResult.IsFailure)
                    return filePathResult.Error;

                var fileContent = new FileContent(file.Content, filePathResult.Value.PathToStorage);

                files.Add(filePathResult.Value, fileContent);
            }

            var fileData = new FileData(files.Values, BUCKET_NAME);

            var uploadResult = await _fileProvider.Uploadfiles(fileData, cancellationToken);
            if (uploadResult.IsFailure)
                return uploadResult.Error;

            foreach (var file in files)
            {
                var petPhotoId = PetPhotoId.NewPetPhotoId();

                var photoResult = PetPhoto.Create(petPhotoId, file.Key, false);
                if (photoResult.IsFailure)
                    return photoResult.Error;

                pet.AddPetPhoto(photoResult.Value);
            }
            
            await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("added pet photos {Nickname} with id {petId} volunteer with id {volunteerId}",
                pet.Nickname,
                petId.Value,
                volunteerResult.Value.Id);

            return (Guid)petId;
        }
    }
}
