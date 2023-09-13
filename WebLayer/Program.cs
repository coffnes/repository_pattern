using Microsoft.Extensions.Options;
using RepoTask.DataAccessLayer;
using RepoTask.BusinessLogicLayer.Mediators;
using RepoTask.DataAccessLayer.Repositories;
using RepoTask.BusinessLogicLayer.Strategies;
using RepoTask.BusinessLogicLayer;
using RepoTask.WebLayer;
using VueCliMiddleware;
using Microsoft.AspNetCore.SpaServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
    name: "_myAllowSpecificOrigins",
    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    );
});

// Add services to the container.
builder.Services.Configure<WeatherDatabaseSettings>(builder.Configuration.GetSection("WeatherDatabase"));
builder.Services.Configure<PlusMongoDatabaseSettings>(builder.Configuration.GetSection("PlusMongoDatabase"));
builder.Services.Configure<MinusMongoDatabaseSettings>(builder.Configuration.GetSection("MinusMongoDatabase"));
builder.Services.Configure<ZeroMongoDatabaseSettings>(builder.Configuration.GetSection("ZeroMongoDatabase"));
builder.Services.AddSingleton((provider) => new MinusMongoClient(provider.GetRequiredService<IOptions<MinusMongoDatabaseSettings>>()));
builder.Services.AddSingleton((provider) => new PlusMongoClient(provider.GetRequiredService<IOptions<PlusMongoDatabaseSettings>>()));
builder.Services.AddSingleton((provider) => new ZeroMongoClient(provider.GetRequiredService<IOptions<ZeroMongoDatabaseSettings>>()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<WeatherHandler>();

builder.Services.AddHostedService((provider) => new WeatherGenerator(provider.GetRequiredService<WeatherHandler>(), provider.GetRequiredService<MongoRepositoryManager>()));

builder.Services.AddTransient<IStrategy<Temperature>, MinusTemperatureStrategy>();
builder.Services.AddTransient<IStrategy<Temperature>, PlusTemperatureStrategy>();
builder.Services.AddTransient<IDefaultStrategy<Temperature>, DefaultTemperatureStrategy>();
builder.Services.AddTransient<IStrategyManager<Temperature>, TemperatureStrategyManager>();

builder.Services.AddTransient<IMediator<string, Temperature>, MinusMediator>();
builder.Services.AddTransient<IMediator<string, Temperature>, PlusMediator>();
builder.Services.AddTransient<IMediator<string, Temperature>, DefaultMediator>();
builder.Services.AddTransient<IMediatorManager<string, Temperature>, TemperatureMediatorManager>();

builder.Services.AddTransient<IMongoRepository<string>, MinusMongoRepository>();
builder.Services.AddTransient<IMongoRepository<string>, PlusMongoRepository>();
builder.Services.AddTransient<IMongoRepository<string>, DefaultMongoRepository>();

builder.Services.AddTransient<IMinusRepository<string>, MinusMongoRepository>();
builder.Services.AddTransient<IPlusRepository<string>, PlusMongoRepository>();
builder.Services.AddTransient<IDefaultRepository<string>, DefaultMongoRepository>();

builder.Services.AddTransient<MongoRepositoryManager>();

var app = builder.Build();

// app.Services.GetRequiredService<Microsoft.AspNetCore.Hosting.IApplicationLifetime>()
//     .ApplicationStarted.Register(app.Services.GetRequiredService<WeatherGenerator>().Generate);
app.UseCors("_myAllowSpecificOrigins");
// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
  app.MapToVueCliProxy(
      "{*path}",
      new SpaOptions { SourcePath = "ClientApp" },
      npmScript: "dev",
      port: 3399,
      regex: "Compiled successfully!",
      forceKill: true,
      wsl: true);
}

if(app.Environment.IsDevelopment()) {
    app.MapWhen(y => y.Request.Path.StartsWithSegments("/app"), client => {
        client.UseSpa(spa =>
            {
                spa.UseProxyToSpaDevelopmentServer("http://localhost:3399");
            });
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
