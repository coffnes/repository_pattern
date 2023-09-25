using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
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
    private readonly IHttpClientFactory _clientFactory;
    public WeatherForecastController(WeatherHandler handler, MongoRepositoryManager repoManager, IHttpClientFactory clientFactory)
    {
        _handler = handler;
        _repoManager = repoManager;
        _clientFactory = clientFactory;
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

    [HttpGet("date/{dateFrom}/{dateTo}")]
    public IList<TemperatureEntity<string>> GetByDate(long dateFrom, long dateTo)
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

    [HttpPost("filter_post")]
    public async void FilterPost() {
        var jsonString = "";
        using(var inputStream = new StreamReader(HttpContext.Request.Body))
        {
            jsonString = await inputStream.ReadToEndAsync();
        }
        FilterOptions? filter = JsonSerializer.Deserialize<FilterOptions>(jsonString);
        var result = _repoManager.GetByFilter(filter);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var jsonResponseString = JsonSerializer.Serialize(result, options);
        await HttpContext.Response.WriteAsync(jsonResponseString);
    }
    [HttpGet("filter_get")]
    public async void FilterGet() {
        FilterOptions filter = new FilterOptions();
        filter.selectedSort = Request.Query["selectedSort"];
        filter.selectedCity = Request.Query["selectedCity"];
        filter.selectedDateFrom = Request.Query["selectedDateFrom"];
        filter.selectedDateTo = Request.Query["selectedDateTo"];
        var result = _repoManager.GetByFilter(filter);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var jsonResponseString = JsonSerializer.Serialize(result, options);
        await HttpContext.Response.WriteAsync(jsonResponseString);
    }
}
