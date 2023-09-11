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

builder.Services.AddSpaStaticFiles(configuration => configuration.RootPath = "../ClientApp/dist");

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
if (app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseSpaStaticFiles();

if (app.Environment.IsDevelopment())
{
  app.MapToVueCliProxy(
      "{*path}",
      new SpaOptions { SourcePath = "../ClientApp" },
      npmScript: "dev",
      regex: "Compiled successfully!");
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSpa(spa => spa.Options.SourcePath = "../ClientApp");

app.MapFallbackToFile("index.html");

app.MapControllers();

app.Run();
