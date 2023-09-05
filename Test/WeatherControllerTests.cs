using Amazon.Runtime.Internal.Util;
using Moq;
using RepoTask.BusinessLogicLayer;
using RepoTask.Controllers;
using RepoTask.DataAccessLayer;
using RepoTask.DataAccessLayer.Repositories;
using Xunit;

namespace RepoTask.Test;

public class WeatherControllerTests
{
    [Fact]
    public void Test()
    {
        var weatherHandlerMock = new Mock<WeatherHandler>();
        weatherHandlerMock.Setup((handler) => handler.Handl(new WeatherForecast())).Returns(Task.CompletedTask);
        //var repositoryManagerMock = new Mock<MongoRepositoryManager>();
        //var controller = new WeatherForecastController(weatherHandlerMock.Object, repositoryManagerMock.Object);
        Assert.Equal(1, 1);
    }
    // private IEnumerable<TemperatureEntity<string>> GetTestUsers()
    // {
    //     return new List<TemperatureEntity<string>>();
    // }
}