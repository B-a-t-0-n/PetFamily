using Microsoft.EntityFrameworkCore;
using PetFamily.Infrastucture;

namespace PetFamily.API.Extensions
{
    public static class AppExtentions
    {
        public static async Task ApplyMigration(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
