using PlatformService.Data;
using Microsoft.EntityFrameworkCore;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

var configuration= builder.Configuration;
var _env = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient<ICommandDataClient,CommandDataClient>();

// Configure AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

if (_env.IsDevelopment())
{
    Console.WriteLine("--> Using InMem DB");
    //Configure DBContext
    builder.Services.AddDbContext<AppDbContext>(options=>
    options.UseInMemoryDatabase("InMemory"));
    //
}
else
{
    System.Console.WriteLine("--> Using SQL server DB");
    builder.Services.AddDbContext<AppDbContext>(options=>
    options.UseSqlServer(configuration.GetConnectionString("PlatformsConnection")));
}

//Configure Servces
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


System.Console.WriteLine($"--> Command Service Endpoint {configuration["CommandService"]}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app,_env.IsProduction());

app.Run();
