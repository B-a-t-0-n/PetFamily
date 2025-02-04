using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstraction;
using PetFamily.Application.FileProvider;
using PetFamily.Application.PetManagement.Queries.GetVolunteersWithPagination;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.AddPet;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.AddPetPhotos;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.DeletePetPhoto;
using PetFamily.Application.PetManagement.UseCases.PetHandlers.MovePet;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Create;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.Delete;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateDetailsForAssistance;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateMainInfo;
using PetFamily.Application.PetManagement.UseCases.VolunteersHandlers.UpdateSocialNetwork;

namespace PetFamily.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddCommands()
                .AddQueries()
                .AddValidatorsFromAssembly(typeof(Inject).Assembly);
        }

        private static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
               .AddClasses(classes => classes
               .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
               .AsSelfWithInterfaces()
               .WithScopedLifetime());

            return services;
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
               .AddClasses(classes => classes
               .AssignableTo(typeof(IQueryHandler<,>)))
               .AsSelfWithInterfaces()
               .WithScopedLifetime());

            return services;
        }
    }
}
