using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.UpdateMainInfo.Requests;
using PetFamily.Domain.PetMenegment.ValueObjects;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.IDs;
using PetFamily.Infrastucture.Repositories;

namespace PetFamily.Application.Volunteers.UpdateMainInfo
{
    public class UpdateMainInfoHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateMainInfoHandler> _logger;

        public UpdateMainInfoHandler(IVolunteerRepository volunteerRepository, ILogger<UpdateMainInfoHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> Handle(UpdateMainInfoRequest request, CancellationToken cancellationToken = default)
        {
            var id = VolunteerId.Create(request.Id);

            var volunteerResult = await _volunteerRepository.GetById(id);
            if(volunteerResult.IsFailure)
                return volunteerResult.Error;

            var fullName = FullName.Create(request.MainInfo.FullName.Name,
                request.MainInfo.FullName.Surname,
                request.MainInfo.FullName.Patronymic).Value;

            var description = Description.Create(request.MainInfo.Description).Value;

            var yearsExperience = YearsExperience.Create(request.MainInfo.YearsExperience).Value;

            var phoneNumder= PhoneNumber.Create(request.MainInfo.PhoneNumber).Value;

            volunteerResult.Value.UpdateMainInfo(fullName, description, yearsExperience, phoneNumder);

            var rezult = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("updated main info volunteer {Surname} {Name} {Patronymic} with id {id}",
                fullName.Surname,
                fullName.Name,
                fullName.Patronymic,
                id.Value);

            return rezult;
        }
    }
}
