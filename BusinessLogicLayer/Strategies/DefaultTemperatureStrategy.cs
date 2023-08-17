using RepoTask.DataAccessLayer;

namespace RepoTask.BusinessLogicLayer.Strategies;

public class DefaultTemperatureStrategy : IDefaultStrategy<Temperature>
{
    public bool Algorithm(Temperature t)
    {
        return false;
    }
}