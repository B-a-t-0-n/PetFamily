//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.Extensions.DependencyInjection;
//using PetFamily.Application.Database;
//using PetFamily.Infrastucture.DbContexts;
//using Testcontainers.PostgreSql;

//namespace PetFamily.Volunteers.IntegrationTests;

//public class IntegrationTestsWebFactory : WebApplicationFactory<Program>, IAsyncLifetime
//{
//    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
//        .WithImage("postgres:13")
//        .WithDatabase("PetFamily")
//        .WithUsername("postgres")
//        .WithPassword("postgres")
//        .Build();

//    protected override void ConfigureWebHost(IWebHostBuilder builder)
//    {
//        builder.ConfigureServices(ConfigureDefaultServices);
//    }

//    protected virtual void ConfigureDefaultServices(IServiceCollection services)
//    {
//        var writeContext = services.SingleOrDefault(s =>
//            s.ServiceType == typeof(WriteDbContext));

//        var readContext = services.SingleOrDefault(s =>
//            s.ServiceType == typeof(IReadDbContext));  

//        if(writeContext is not null)
//            services.Remove(writeContext);

//        if (readContext is not null)
//            services.Remove(readContext);

//        //services.AddScoped<WriteDbContext>(_ =>
//        //    new WriteDbContext());

//        //services.AddScoped<IReadDbContext, ReadDbContext>(_ =>
//        //    new ReadDbContext(builder.Configuration));
//    }

//    public async Task InitializeAsync()
//    {
//        await _dbContainer.StartAsync();
//    }

//    async Task IAsyncLifetime.DisposeAsync()
//    {
//        await _dbContainer.StopAsync();
//        await _dbContainer.DisposeAsync();
//    }
//}
