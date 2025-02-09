using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Application.Extentions;
using PetFamily.Application.PetManagement.Commands.VolunteersHandlers.HardDelete.Commands;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Messaging;
using PetFamily.Application.Providers;

namespace PetFamily.Application.PetManagement.Commands.VolunteersHandlers.HardDelete
{
    public class HardDeleteVolunteerHandler : ICommandHandler<HardDeleteVolunteerCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<HardDeleteVolunteerHandler> _logger;
        private readonly IValidator<HardDeleteVolunteerCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileProvider _fileProvider;


        public HardDeleteVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<HardDeleteVolunteerHandler> logger,
            IValidator<HardDeleteVolunteerCommand> validator,
            IUnitOfWork unitOfWork,
            IFileProvider fileProvider)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _fileProvider = fileProvider;
        }

        public async Task<UnitResult<ErrorList>> Handle(HardDeleteVolunteerCommand command, CancellationToken cancellationToken = default)
        {
            var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

            try
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                if (validationResult.IsValid == false)
                {
                    return validationResult.ToErrorList();
                }

                var id = VolunteerId.Create(command.Id);

                var volunteerResult = await _volunteerRepository.GetById(id);
                if (volunteerResult.IsFailure)
                    return volunteerResult.Error.ToErrorList();

                var filesMetadata = volunteerResult.Value.Pets
                    .SelectMany(p => p.PetPhotos
                        .Select(photo => new FileMetadata(Constants.BUCKET_NAME, photo.Path.PathToStorage)))
                    .ToList();

                _volunteerRepository.Delete(volunteerResult.Value);
                await _unitOfWork.SaveChanges(cancellationToken);

                foreach (var fileMetadata in filesMetadata)
                {
                    var deleteResult = await _fileProvider.Deletefile(fileMetadata, cancellationToken);
                    if (deleteResult.IsFailure)
                        return deleteResult.Error.ToErrorList();
                }
                
                transaction.Commit();

                _logger.LogInformation("hard deleted volunteer with id {id}", id.Value);

                return Result.Success<ErrorList>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Can not delete volunteer - {id} in transaction", command.Id);

                transaction.Rollback();
                return Error.Failure("Can not delete volunteer", "volunteer.delete.failure").ToErrorList();
            }
            
        }
    }
}
