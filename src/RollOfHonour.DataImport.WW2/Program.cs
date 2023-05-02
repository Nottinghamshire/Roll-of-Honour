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

// List<WW2Data> militaryResults = ww2DataFetchService.ProperWW2Data();
// if (militaryResults.Any())
// {
//     var errorRows = await ww2DataInsertService?.AddMissingData(militaryResults)!;
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


List<WW2Data> civilianResults = ww2DataFetchService.CivilianWarDeadWW2Data();
if (civilianResults.Any())
{
    var errorRows = await ww2DataInsertService?.AddMissingCivilianData(civilianResults)!;

    if (errorRows.Any())
    {
        Console.WriteLine("Press a key to list Errors");
        Console.ReadLine();

        Console.WriteLine("ERRORS:");
        foreach (var errorRow in errorRows)
        {
            Console.WriteLine(errorRow.MaybeCWGCRef + "'. " + errorRow.FamilyInfo?.ToString());
        }

        Console.ReadLine();
    }
}


await host.RunAsync();
