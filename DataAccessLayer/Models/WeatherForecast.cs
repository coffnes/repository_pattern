namespace RepoTask.DataAccessLayer;

public class WeatherForecast : TemperatureEntity<string>
{
    public WeatherForecast()
    {
        Id = Guid.NewGuid().ToString();
    }

    public DateOnly Date { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }

    public string? City { get; set; }
}
