using Microsoft.Extensions.DependencyInjection;
using PetFamily.Infrastucture.Interceptors;
using PetFamily.Infrastucture.Repositories;

namespace PetFamily.Infrastucture
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();

            return services;
        }
    }
}
