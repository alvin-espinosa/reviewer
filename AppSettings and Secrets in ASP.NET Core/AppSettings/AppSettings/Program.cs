using AppSettings.Configurations;
using AppSettings.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//// For Global use, thru IOptions
//var auth0 = builder.Configuration.GetSection("Auth0");
//builder.Services.Configure<Auth0Settings>(auth0);
//// 

//// set up to allow DI
//var auth0 = new Auth0Settings();
//new ConfigureFromConfigurationOptions<Auth0Settings>(builder.Configuration.GetSection("Auth0"))
//    .Configure(auth0);
//builder.Services.AddSingleton(auth0);

//// ConnectionStrings
var database1 =  builder.Configuration.GetConnectionString("database1");
///
    
builder.Services.AddConfiguration<Auth0Settings>(builder.Configuration, "Auth0");


var app = builder.Build();

builder.Host.ConfigureAppConfiguration(options => {
    options.Sources.Clear();
    options.AddJsonFile($"appsettings.json", false, true);
    options.AddJsonFile($"appsettings.{app.Environment.EnvironmentName}.json", false, true);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
