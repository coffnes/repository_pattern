namespace RepoTask.Models;

public class MinusTemperatureStrategy : IMinusStrategy<Temperature>
{
    public bool Algorithm(Temperature t)
    {
        return t.TemperatureC < 0;
    }
}