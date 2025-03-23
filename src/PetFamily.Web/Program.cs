using Microsoft.OpenApi.Models;
using PetFamily.Accounts.Infrastructure.Seeders;
using PetFamily.Accounts.Presentation;
using PetFamily.Species.Presentation;
using PetFamily.Volunteers.Presentation;
using Serilog;
using Serilog.Events;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")
                 ?? throw new ArgumentException("Seq"))
    .Enrich.WithThreadId()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentUserName()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
              new OpenApiSecurityScheme
              {
                 Reference = new OpenApiReference
                 {
                   Type = ReferenceType.SecurityScheme,
                   Id = "Bearer"
                 }
               },
               new string[] { }
             }
            });
});
builder.Services.AddSerilog();

builder.Services
    .AddAccountPresentation(builder.Configuration)
    .AddVolunteersPresentation(builder.Configuration)
    .AddSpeciesPresentation(builder.Configuration);

var app = builder.Build();

var accountsSeeeder = app.Services.GetRequiredService<AccountsSeeder>();

await accountsSeeeder.SeedAsync();

app.UseExceptionMiddleware();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program;