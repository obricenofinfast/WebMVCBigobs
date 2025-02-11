using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;


var builder = WebApplication.CreateBuilder(args);

// Conectar a Azure App Configuration





/*
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(new Uri(appConfigEndpoint), new DefaultAzureCredential())
        .Select("ConnectionStrings:*");
});
*/
var appConfigEndpoint = builder.Configuration["AppConfig:Endpoint"];

builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(new Uri(appConfigEndpoint), new DefaultAzureCredential())
        .Select("Secret:*");
});

/*
var appConfigEndpoint = builder.Configuration["AppConfig:Endpoint"];
 
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect("Endpoint=https://llavefinfast.azconfig.io;Id=Q4aD;Secret=9XeCLH2OlmwI1hRQRWcshdJv8KgGdit4I3wyNTYiZtcW5jOPXnJ2JQQJ99BAACYeBjFAwFTjAAACAZACRH4X")
        .Select(KeyFilter.Any);
});
*/

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();