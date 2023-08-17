namespace RepoTask.Models;

public class DefaultTemperatureStrategy : IDefaultStrategy<Temperature>
{
    public bool Algorithm(Temperature t)
    {
        return false;
    }
}