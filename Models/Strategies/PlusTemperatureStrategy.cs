namespace RepoTask.Models;

public class PlusTemperatureStrategy : IPlusStrategy<Temperature>
{
    public bool Algorithm(Temperature t)
    {
        return t.TemperatureC > 0;
    }
}