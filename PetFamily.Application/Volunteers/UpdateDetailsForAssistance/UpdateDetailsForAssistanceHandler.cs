using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.UpdateSocialNetwork.Commands;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Infrastucture.Repositories;
using PetFamily.Domain.Shared;
using PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Commands;
using PetFamily.Application.Database;

namespace PetFamily.Application.Volunteers.UpdateDetailsForAssistance
{
    public class UpdateDetailsForAssistanceHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateDetailsForAssistanceHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDetailsForAssistanceHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<UpdateDetailsForAssistanceHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, Error>> Handle(UpdateDetailsForAssistanceCommand command, CancellationToken cancellationToken = default)
        {
            var id = VolunteerId.Create(command.Id);

            var volunteerResult = await _volunteerRepository.GetById(id);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var detailsForAssistanceList = new List<DetailsForAssistance>();

            if (command.DetailsForAssistanceDto.DetailsForAssistance != null)
            {
                foreach (var detailsForAssistanceItem in command.DetailsForAssistanceDto.DetailsForAssistance)
                {
                    var detailsForAssistance = DetailsForAssistance.Create(detailsForAssistanceItem.Name, detailsForAssistanceItem.Description).Value;

                    detailsForAssistanceList.Add(detailsForAssistance);
                }
            }

            volunteerResult.Value.UpdateDetailsForAssistance(detailsForAssistanceList);

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("updated details for assistance volunteer {Surname} {Name} {Patronymic} with id {id}",
                volunteerResult.Value.FullName.Surname,
                volunteerResult.Value.FullName.Name,
                volunteerResult.Value.FullName.Patronymic,
                id.Value);

            return id.Value;
        }
    }
}
