using Dapper;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Dtos;
using PetFamily.Infrastucture;
using System.Text.Json;

namespace PetFamily.Application.PetManagement.Queries.VolunteerHandlers.GetVolunteerById
{
    public class GetVolunteerByIdHandler : IQueryHandler<VolunteerDto?, GetVolunteerByIdQuery>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly ILogger<GetVolunteerByIdHandler> _logger;

        public GetVolunteerByIdHandler(
            ISqlConnectionFactory connectionFactory,
            ILogger<GetVolunteerByIdHandler> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<VolunteerDto?> Handle(GetVolunteerByIdQuery query, CancellationToken cancellation = default)
        {
            var connection = _connectionFactory.Create();

            var parameters = new DynamicParameters();

            var sql = """
                        SELECT 
                            v.id,
                            v.name,
                            v.surname,
                            v.patronymic,
                            v.description,
                            v.years_experience,
                            v.phone_number,
                            v.details_for_assistance,
                            v.social_network
                        FROM volunteer v
                        WHERE v.id = @Id and v.is_deleted = false
                        """;
            parameters.Add("@Id", query.Id);

            var volunteer = await connection.QueryAsync<VolunteerDto, string, string, VolunteerDto>(
                sql,
                (volunteer, detailsForAssistanceJson, socialNetworkJson) =>
                {
                    var detailsForAssistance = JsonSerializer.Deserialize<DetailsForAssistanceDto[]>(detailsForAssistanceJson, JsonSerializerOptions.Default) ?? [];
                    var socialNetwork = JsonSerializer.Deserialize<SocialNetworkDto[]>(socialNetworkJson, JsonSerializerOptions.Default) ?? [];

                    volunteer.SocialNetwork = socialNetwork;
                    volunteer.DetailsForAssistance = detailsForAssistance;

                    return volunteer;
                },

                splitOn: "details_for_assistance, social_network",
                param: parameters);

            _logger.LogInformation("received volunteer with id {id}", query.Id);
            
            return volunteer.Any() ? volunteer.First() : null;
        }
    }
}
