using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.Accounts.Application.Queries;

public class GetUserByIdHandler : IQueryHandler<UserDto?, GetUserByIdQuery>
{
    private readonly IReadAccountsDbContext _readDbContext;
    private readonly ILogger<GetUserByIdHandler> _logger;


    public GetUserByIdHandler(
        IReadAccountsDbContext readDbContext,
        ILogger<GetUserByIdHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<UserDto?> Handle(
        GetUserByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var user = await _readDbContext.Users
            .Include(u => u.AdminAccount)
            .Include(u => u.PartisipantAccount)
            .Include(u => u.VolunteerAccount)
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (user == null)
            return null;

        _logger.LogInformation("received user with id {id}", query.Id);

        var userDto = new UserDto
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            Photo = user.Photo,
            Name = user.FullName.FirstName,
            Surname = user.FullName.Surname,
            Patronymic = user.FullName.Patronymic,
            Roles = user.Roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name!
            }),
            SocialNetworks = user.SocialNetworks.Select(sn => new SocialNetworkDto
            {
                Name = sn.Name,
                Link = sn.Link
            }),
            AdminAccount = user.AdminAccount == null ? null : new AdminAccountDto
            {
                Id = user.AdminAccount.Id,
                UserId = user.AdminAccount.UserId,
            },
            PartisipantAccount = user.PartisipantAccount == null ? null : new PartisipantAccountDto
            {
                Id = user.PartisipantAccount.Id,
                FavoritePets = user.PartisipantAccount.FavoritePets,
                UserId = user.PartisipantAccount.UserId
            },
            VolunteerAccount = user.VolunteerAccount == null ? null : new VolunteerAccountDto
            {
                Id = user.VolunteerAccount.Id,
                Certificates = user.VolunteerAccount.Certificates,
                UserId = user.VolunteerAccount.UserId,
                YearsExperience = user.VolunteerAccount.YearsExperience.Value,
                Requisites = user.VolunteerAccount.Requisites.Select(r => new RequisitesDto
                {
                    Name = r.Name,
                    Description = r.Description
                })
            }
        };

        return userDto;
    }
}