using RepoTask.DataAccessLayer;
using RepoTask.DataAccessLayer.Repositories;
using RepoTask.BusinessLogicLayer.Strategies;

namespace RepoTask.BusinessLogicLayer.Mediators;

public class MinusMediator : IMediator<string, Temperature>
{
    private readonly IMinusRepository<string> _repository;

    public MinusMediator(IMinusRepository<string> repository)
    {
        _repository = repository;
    }

    public IMongoRepository<string> GetRepository()
    {
        return _repository;
    }

    public bool StrategyTypeCheck(IStrategy<Temperature> s)
    {
        return s is IMinusStrategy<Temperature>;
    }
}