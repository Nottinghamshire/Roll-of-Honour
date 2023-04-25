using Microsoft.EntityFrameworkCore;
using RollOfHonour.Data.Context;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RollOfHonour.DataImport.WW2;


var appSettingsConfig = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var host = Host.CreateDefaultBuilder()
    // .ConfigureAppConfiguration((hostingContext, config) =>
    // {
    //     config.AddAzureAppConfiguration(options =>
    //     {
    //         options.Connect(appSettingsConfig.GetConnectionString("AzureAppConfiguration"))
    //             .Select(KeyFilter.Any, LabelFilter.Null)
    //             .Select(KeyFilter.Any, appSettingsConfig["EnvironmentName"]);
    //     });
    // })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<RollOfHonourContext>(options =>
        {
            //             options.UseSqlServer(appSettingsConfig.GetConnectionString("ConnectionStrings:DefaultConnection"))
            options.UseSqlServer(hostContext.Configuration["ConnectionStrings:DefaultConnection"]);
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }, ServiceLifetime.Transient);
        services.AddTransient<IWW2ImportRepository, WW2ImportRepository>();
        services.AddTransient<IDataInsertService, DataInsertService>();
    })
    .UseConsoleLifetime()
    .Build();


IWW2ImportRepository ww2DataFetchService = host.Services.GetService<IWW2ImportRepository>()!;
IDataInsertService? ww2DataInsertService = host.Services.GetService<IDataInsertService>();

List<WW2Data> results = ww2DataFetchService.ProperWW2Data();
if (results.Any())
{
    await ww2DataInsertService?.AddMissingData(results)!;
}


await host.RunAsync();
