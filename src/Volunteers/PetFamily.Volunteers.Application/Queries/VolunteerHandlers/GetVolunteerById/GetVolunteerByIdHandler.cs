using Dapper;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Queries.VolunteerHandlers.GetVolunteerById;

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
                    v.description,
                    v.phone_number,
                FROM volunteers.volunteer v
                WHERE v.id = @Id and v.is_deleted = false
                """;
        parameters.Add("@Id", query.Id);

        var volunteer = await connection.QueryAsync<VolunteerDto>(sql);

        _logger.LogInformation("received volunteer with id {id}", query.Id);
    
        return volunteer.Any() ? volunteer.First() : null;
    }
}
