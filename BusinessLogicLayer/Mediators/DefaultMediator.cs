using RepoTask.DataAccessLayer;
using RepoTask.DataAccessLayer.Repositories;
using RepoTask.BusinessLogicLayer.Strategies;

namespace RepoTask.BusinessLogicLayer.Mediators;

public class DefaultMediator : IMediator<string, Temperature>
{
    private readonly IDefaultRepository<string> _repository;

    public DefaultMediator(IDefaultRepository<string> repository)
    {
        _repository = repository;
    }

    public IMongoRepository<string> GetRepository()
    {
        return _repository;
    }

    public bool StrategyTypeCheck(IStrategy<Temperature> s)
    {
        return s is IDefaultStrategy<Temperature>;
    }
}