using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Application.Providers;
using PetFamily.Application.PetManagement.Commands.PetHandlers.HardDeletePet.Commands;
using PetFamily.Application.Extentions;

namespace PetFamily.Application.PetManagement.Commands.PetHandlers.HardDeletePet
{
    public class HardDeletePetHandler : ICommandHandler<HardDeletePetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<HardDeletePetHandler> _logger;
        private readonly IValidator<HardDeletePetCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileProvider _fileProvider;


        public HardDeletePetHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<HardDeletePetHandler> logger,
            IValidator<HardDeletePetCommand> validator,
            IUnitOfWork unitOfWork,
            IFileProvider fileProvider)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _fileProvider = fileProvider;
        }

        public async Task<UnitResult<ErrorList>> Handle(HardDeletePetCommand command, CancellationToken cancellationToken = default)
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

                var petId = PetId.Create(command.PetId); 
                
                var pet = volunteerResult.Value.Pets.FirstOrDefault(p => p.Id == petId);
                if (pet is null)
                    return Errors.General.NotFound(petId).ToErrorList();

                var filesMetadata = pet.PetPhotos
                    .Select(p => new FileMetadata(Constants.BUCKET_NAME, p.Path.PathToStorage))
                    .ToList();

                var result = volunteerResult.Value.HardDeletePet(petId);
                if (result.IsFailure)
                    return result.Error.ToErrorList();

                await _unitOfWork.SaveChanges(cancellationToken);

                foreach (var fileMetadata in filesMetadata)
                {
                    var deleteResult = await _fileProvider.Deletefile(fileMetadata, cancellationToken);
                    if (deleteResult.IsFailure)
                        return deleteResult.Error.ToErrorList();
                }

                transaction.Commit();

                _logger.LogInformation("hard deleted pet with id {id}", petId);

                return Result.Success<ErrorList>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Can not delete pet - {id} in transaction", command.PetId);

                transaction.Rollback();
                return Error.Failure("Can not delete pet", "pet.delete.failure").ToErrorList();
            }

        }
    }
}
