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
            // options.UseSqlServer(hostContext.Configuration[@"Server=tcp:asql-corp-prd-01.database.windows.net,1433;Initial Catalog=SQLDB-RollOfHonour-PRD;Persist Security Info=False;User ID=RollOfHonour;Password=hFXoW3KBGwyNaKTRdAnr;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"]);
            options.UseSqlServer(hostContext.Configuration["ConnectionStrings:DefaultConnection"],
                x => x.UseNetTopologySuite());
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }, ServiceLifetime.Transient);
        services.AddTransient<IWW2ImportRepository, WW2ImportRepository>();
        services.AddTransient<IDataInsertService, DataInsertService>();
        services.AddTransient<IDataUpdateService, DataUpdateService>();
    })
    .UseConsoleLifetime()
    .Build();


IWW2ImportRepository ww2DataFetchService = host.Services.GetService<IWW2ImportRepository>()!;
IDataInsertService? ww2DataInsertService = host.Services.GetService<IDataInsertService>();
IDataUpdateService? dataUpdateService = host.Services.GetService<IDataUpdateService>();

// List<WW2Data> results = ww2DataFetchService.ProperWW2Data();
// if (results.Any())
// {
//     var errorRows = await ww2DataInsertService?.AddMissingData(results)!;
//
//     if (errorRows.Any())
//     {
//         Console.WriteLine("Press a key to list Errors");
//         Console.ReadLine();
//         
//         Console.WriteLine("ERRORS:");
//         foreach (var errorRow in errorRows)
//         {
//             Console.WriteLine(errorRow.Service_Number?.ToString());
//         }
//
//         Console.ReadLine();
//     }
// }

await dataUpdateService?.UpdateGeographicTypeFromEastingNorthing()!;

await host.RunAsync();
