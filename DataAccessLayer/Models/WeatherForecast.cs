namespace RepoTask.DataAccessLayer;

public class WeatherForecast : TemperatureEntity<string>
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
