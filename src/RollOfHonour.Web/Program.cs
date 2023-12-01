using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Logging;
using RollOfHonour.Core;
using RollOfHonour.Core.Search;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Context;
using RollOfHonour.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddAzureAppConfiguration(builder.Configuration.GetConnectionString("AzureAppConfiguration"));

builder.Services.AddDbContext<RollOfHonourContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.UseNetTopologySuite()));

builder.Services.Configure<Storage>(builder.Configuration.GetSection(nameof(AppSettings.Storage)));
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<IMemorialRepository, MemorialRepository>();
builder.Services.AddTransient<ISuperSearchService, SuperSearchService>();

builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAdB2C");

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = options.DefaultPolicy;
//});

builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

builder.Services.AddSignalR().AddAzureSignalR(options =>
{
    options.ServerStickyMode = Microsoft.Azure.SignalR.ServerStickyMode.Required;
});

builder.Services.AddServerSideBlazor();

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy(AuthorizationPolicyNames.EditPerson, policy => 
//        policy.RequireAssertion(context => 
//            context.User.HasClaim(claim => 
//                claim.Type is AuthorizationClaims.AdminPersonEdit 
//                    or AuthorizationClaims.ModeratorPersonEdit)));

//    options.AddPolicy(AuthorizationPolicyNames.EditMemorial, policy => 
//        policy.RequireAssertion(context => 
//            context.User.HasClaim(claim => 
//                claim.Type is AuthorizationClaims.AdminMemorialEdit 
//                    or AuthorizationClaims.ModeratorMemorialEdit)));
//});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<RollOfHonourContext>();
    context.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "MemorialRemoveCitizen",
        pattern: "{controller=Memorial}/{action=Remove}/{memorialId?}/{citizenId?}");
});

app.MapRazorPages();
app.MapBlazorHub();
app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();
