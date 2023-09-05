using Microsoft.Extensions.Hosting;
using RepoTask.DataAccessLayer;
using RepoTask.DataAccessLayer.Repositories;
using RepoTask.BusinessLogicLayer;

namespace RepoTask.Test;

public class WeatherGenerator : IHostedService
{
    private readonly WeatherHandler _handler;
    private readonly Random rnd;
    private readonly MongoRepositoryManager _manager;

    public WeatherGenerator(WeatherHandler handler, MongoRepositoryManager manager)
    {
        _manager = manager;
        _handler = handler;
        rnd = new();
    }

    public async Task Delete()
    {
        await _manager.DeleteAll();
    }

    public void Generate()
    {
        // for(int i = 0; i < 40; i++)
        // {
        //     Thread t = new Thread(new ThreadStart(GeneratePlusTemperatures));
        //     await GeneratePlusTemperatures();
        //     await GenerateMinusTemperatures();
        //     await GenerateZeroTemperatures();
        // }
        for(int i = 0; i < -1; i++)
        {
            ThreadPool.QueueUserWorkItem((state) => GeneratePlusTemperatures());
            ThreadPool.QueueUserWorkItem((state) => GenerateMinusTemperatures());
            ThreadPool.QueueUserWorkItem((state) => GenerateZeroTemperatures());
        }
    }

    public void GeneratePlusTemperatures()
    {
        List<TemperatureEntity<string>> chunk = new();
        for(int i = 0; i < 250; i++)
        {
            WeatherForecast weather = new()
            {
                TemperatureC = GenerateTemperature(),
                City = GenerateCity(),
                Date = GenerateDate()
            };
            chunk.Add(weather);
        }
        Temperature t = new()
        {
            TemperatureC = 1,
        };
        _handler.HandlChunk(chunk, t);
    }
    public void GenerateMinusTemperatures()
    {
        List<TemperatureEntity<string>> chunk = new();
        for(int i = 0; i < 250; i++)
        {
            WeatherForecast weather = new()
            {
                TemperatureC = (-1)*GenerateTemperature(),
                City = GenerateCity(),
                Date = GenerateDate()
            };
            chunk.Add(weather);
        }
        Temperature t = new()
        {
            TemperatureC = -1,
        };
        _handler.HandlChunk(chunk, t);
    }
    public void GenerateZeroTemperatures()
    {
        List<TemperatureEntity<string>> chunk = new();
        for(int i = 0; i < 250; i++)
        {
            WeatherForecast weather = new()
            {
                TemperatureC = 0,
                City = GenerateCity(),
                Date = GenerateDate()
            };
            chunk.Add(weather);
        }
        Temperature t = new()
        {
            TemperatureC = 0,
        };
        _handler.HandlChunk(chunk, t);
    }

    private int GenerateTemperature()
    {
        return rnd.Next(1, 35);
    }

    private string GenerateCity()
    {
        List<string> cities = new(){ "Moscow", "Saint-Petesburg", "Novosibirsk", "Yekaterinburg", "Kazan", "Nizhny Novgorod" };
        return cities[rnd.Next(cities.Count - 1)];
    }

    private DateOnly GenerateDate()
    {
        DateTime start = new(2023, 8, 1);
        DateTime stop = new(2023, 8, 31);
        int range = (stop - start).Days;
        return DateOnly.FromDateTime(start.AddDays(rnd.Next(range)));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Delete();
        Generate();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Delete();
    }
}