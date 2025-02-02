using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<CreateVolunteerHandler>();
            services.AddScoped<UpdateMainInfoHandler>();
            services.AddScoped<UpdateSocialNetworkHandler>();
            services.AddScoped<UpdateDetailsForAssistanceHandler>();
            services.AddScoped<DeleteVolunteerHandler>();
            services.AddScoped<AddPetHandler>();
            services.AddScoped<AddPetPhotosHandler>();
            services.AddScoped<DeletePetPhotoHandler>();
            services.AddScoped<MovePetHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
