using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Context;
using RollOfHonour.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddAzureAppConfiguration(builder.Configuration.GetConnectionString("AzureAppConfiguration"));

builder.Services.AddDbContext<RollOfHonourContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IMemorialRepository, MemorialRepository>();

//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    //.AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

//builder.Services.AddAuthorization(options =>
//{
    //options.FallbackPolicy = options.DefaultPolicy;
//});
builder.Services.AddRazorPages();
    //.AddMicrosoftIdentityUI();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
