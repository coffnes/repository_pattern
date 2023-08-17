using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using RepoTask.DataAccessLayer;

namespace RepoTask.BusinessLogicLayer;

public class WeatherGenerator : IHostedService
{
    private readonly WeatherHandler _handler;
    private readonly Random rnd;
    private readonly WeatherDataAccessor _weatherDataAccessor;

    public WeatherGenerator(WeatherHandler handler, WeatherDataAccessor weatherDataAccessor)
    {
        _weatherDataAccessor = weatherDataAccessor;
        _handler = handler;
        rnd = new();
    }

    public async Task Delete()
    {
        await _weatherDataAccessor.DeleteAll();
    }

    public async Task Generate()
    {
        await GeneratePlusTemperatures();
        await GenerateMinusTemperatures();
        await GenerateZeroTemperatures();
    }

    public async Task GeneratePlusTemperatures()
    {
        for(int i = 0; i < 100; i++)
        {
            WeatherForecast weather = new()
            {
                TemperatureC = GenerateTemperature(),
                City = GenerateCity(),
                Date = GenerateDate()
            };
            await _handler.Handl(weather);
        }
    }
    public async Task GenerateMinusTemperatures()
    {
        for(int i = 0; i < 100; i++)
        {
            WeatherForecast weather = new()
            {
                TemperatureC = (-1)*GenerateTemperature(),
                City = GenerateCity(),
                Date = GenerateDate()
            };
            await _handler.Handl(weather);
        }
    }
    public async Task GenerateZeroTemperatures()
    {
        for(int i = 0; i < 100; i++)
        {
            WeatherForecast weather = new()
            {
                TemperatureC = 0,
                City = GenerateCity(),
                Date = GenerateDate()
            };
            await _handler.Handl(weather);
        }
    }

    private int GenerateTemperature()
    {
        
        ;
        return rnd.Next(1, 35);
    }

    private string GenerateCity()
    {
        List<string> cities = new(){ "Moscow", "Saint-Petesburg", "Novosibirsk", "Yekaterinburg", "Kazan", "Nizhny Novgorod" };
        return cities[rnd.Next(cities.Count - 1)];
    }

    private DateOnly GenerateDate()
    {
        DateTime start = new DateTime(2023, 8, 1);
        DateTime stop = new DateTime(2023, 8, 31);
        int range = (stop - start).Days;
        return DateOnly.FromDateTime(start.AddDays(rnd.Next(range)));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Delete();
        await Generate();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Delete();
    }
}