using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.UpdateDetailsForAssistance.Commands;
using PetFamily.Application.Volunteers.UpdateDetailsForAssistance;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Infrastucture.Repositories;
using PetFamily.Domain.Shared;
using PetFamily.Application.Volunteers.Delete.Commands;

namespace PetFamily.Application.Volunteers.Delete
{
    public class DeleteVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<DeleteVolunteerHandler> _logger;

        public DeleteVolunteerHandler(IVolunteerRepository volunteerRepository, ILogger<DeleteVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> Handle(DeleteVolunteerCommand request, CancellationToken cancellationToken = default)
        {
            var id = VolunteerId.Create(request.Id);

            var volunteerResult = await _volunteerRepository.GetById(id);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var rezult = await _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("deleted volunteer {Surname} {Name} {Patronymic} with id {id}",
                volunteerResult.Value.FullName.Surname,
                volunteerResult.Value.FullName.Name,
                volunteerResult.Value.FullName.Patronymic,
                id.Value);

            return rezult;
        }
    }
}
