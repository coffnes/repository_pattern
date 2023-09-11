using Microsoft.AspNetCore.Mvc;
using RepoTask.BusinessLogicLayer;
using RepoTask.DataAccessLayer;
using RepoTask.DataAccessLayer.Repositories;

namespace RepoTask.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly WeatherHandler _handler;
    private readonly MongoRepositoryManager _repoManager;
    public WeatherForecastController(WeatherHandler handler, MongoRepositoryManager repoManager)
    {
        _handler = handler;
        _repoManager = repoManager;
    }

    [HttpGet]
    public IEnumerable<TemperatureEntity<string>> Get()
    {
        return _repoManager.GetAll();
    }

    [HttpGet("city/{city?}")]
    public IList<TemperatureEntity<string>> GetByCity(string? city)
    {
        return _repoManager.GetByCity(city);
    }

    [HttpGet("date/{dateFrom}-{dateTo}")]
    public IList<TemperatureEntity<string>> GetByDate(DateOnly dateFrom, DateOnly dateTo)
    {
        return _repoManager.GetByDate(dateFrom, dateTo);
    }

    [HttpGet("zero")]
    public IList<TemperatureEntity<string>> GetOnlyZeroTemperature()
    {
        return _repoManager.GetOnlyZeroTemperature();
    }

    [HttpPost]
    public async void Post(WeatherForecast weatherForecast)
    {
        await _handler.Handl(weatherForecast);
    }
}
