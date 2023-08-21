using Microsoft.AspNetCore.Mvc;
using RepoTask.BusinessLogicLayer;
using RepoTask.DataAccessLayer;

namespace RepoTask.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly WeatherHandler _handler;
    private readonly WeatherDataAccessor _weatherDataAccessor;
    public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherHandler handler, WeatherDataAccessor weatherDataAccessor)
    {
        _logger = logger;
        _handler = handler;
        _weatherDataAccessor = weatherDataAccessor;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return _weatherDataAccessor.GetAll();
    }

    [HttpGet("city/{city?}")]
    public IList<WeatherForecast> GetByCity(string? city)
    {
        return _weatherDataAccessor.GetByCity(city);
    }

    [HttpGet("date/{dateFrom}-{dateTo}")]
    public IList<WeatherForecast> GetByDate(DateOnly dateFrom, DateOnly dateTo)
    {
        return _weatherDataAccessor.GetByDate(dateFrom, dateTo);
    }

    [HttpGet("zero")]
    public IList<WeatherForecast> GetOnlyZeroTemperature()
    {
        return _weatherDataAccessor.GetOnlyZeroTemperature();
    }

    [HttpPost]
    public void Post(WeatherForecast weatherForecast)
    {
        _handler.Handl(weatherForecast);
    }
}
