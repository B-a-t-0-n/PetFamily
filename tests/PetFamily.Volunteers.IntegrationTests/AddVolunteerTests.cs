using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Database;

namespace PetFamily.Volunteers.IntegrationTests
{
    public class AddVolunteerTests : IClassFixture<IntegrationTestsWebFactory>
    {
        private readonly IntegrationTestsWebFactory _factory;

        public AddVolunteerTests(IntegrationTestsWebFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test1()
        {
            var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IReadDbContext>();

            var volunteer = await dbContext.Volunteers.ToListAsync();

            volunteer.Should().BeEmpty();
        } 
    }
}