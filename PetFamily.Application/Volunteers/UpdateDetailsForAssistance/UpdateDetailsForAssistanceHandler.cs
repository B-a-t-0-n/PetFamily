using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.UpdateSocialNetwork.Requests;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Infrastucture.Repositories;
using PetFamily.Domain.Shared;
using PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Requests;

namespace PetFamily.Application.Volunteers.UpdateDetailsForAssistance
{
    public class UpdateDetailsForAssistanceHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateDetailsForAssistanceHandler> _logger;

        public UpdateDetailsForAssistanceHandler(IVolunteerRepository volunteerRepository, ILogger<UpdateDetailsForAssistanceHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> Handle(UpdateDetailsForAssistanceRequest request, CancellationToken cancellationToken = default)
        {
            var id = VolunteerId.Create(request.Id);

            var volunteerResult = await _volunteerRepository.GetById(id);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var detailsForAssistanceList = new List<DetailsForAssistance>();

            if (request.DetailsForAssistanceDto.DetailsForAssistance != null)
            {
                foreach (var detailsForAssistanceItem in request.DetailsForAssistanceDto.DetailsForAssistance)
                {
                    var detailsForAssistance = DetailsForAssistance.Create(detailsForAssistanceItem.Name, detailsForAssistanceItem.Description).Value;

                    detailsForAssistanceList.Add(detailsForAssistance);
                }
            }

            var volunteerDetailsForAssistance = new VolunteerDetailsForAssistance(detailsForAssistanceList);

            volunteerResult.Value.UpdateDetailsForAssistance(volunteerDetailsForAssistance);

            var rezult = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("updated details for assistance volunteer {Surname} {Name} {Patronymic} with id {id}",
                volunteerResult.Value.FullName.Surname,
                volunteerResult.Value.FullName.Name,
                volunteerResult.Value.FullName.Patronymic,
                id.Value);

            return rezult;
        }
    }
}
