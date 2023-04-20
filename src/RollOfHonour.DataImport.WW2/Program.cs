using Microsoft.EntityFrameworkCore;
using RollOfHonour.Data.Context;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RollOfHonour.DataImport.WW2;

Console.WriteLine("Hello starting world!");


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
             options.UseSqlServer(hostContext.Configuration["ConnectionStrings:DefaultConnection"])
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
         }, ServiceLifetime.Transient);
         services.AddTransient<IWW2ImportRepository, WW2ImportRepository>();
     })
    .UseConsoleLifetime()
    .Build();

Console.WriteLine("Hello pre-query world!");

var someService = host.Services.GetService<IWW2ImportRepository>();

List<WW2Data>? results = someService?.ProperWW2Data();

if (results != null)
{
    foreach (var line in results)
    {
        Console.WriteLine(line?.Service_Number?.ToString());
    }
}

await host.RunAsync();

