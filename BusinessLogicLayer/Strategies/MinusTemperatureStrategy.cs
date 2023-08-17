using RepoTask.DataAccessLayer;

namespace RepoTask.BusinessLogicLayer.Strategies;

public class MinusTemperatureStrategy : IMinusStrategy<Temperature>
{
    public bool Algorithm(Temperature t)
    {
        return t.TemperatureC < 0;
    }
}