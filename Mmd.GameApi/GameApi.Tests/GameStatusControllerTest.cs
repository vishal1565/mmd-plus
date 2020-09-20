using DataAccess.Data;
using DataAccess.Data.Services;
using DataAccess.Model;
using GameApi.Tests.Helpers;
using Moq;
using Xunit;

namespace GameApi.Tests
{
    public class GameStatusControllerTest
    {
        [Fact]
        public async void GetGameStatusTest()
        {
            //Arrange
            var dbContext = new Mock<DataContext>();
            dbContext.SetupGet(x => x.Teams).Returns(TestFunctions.GetDbSet<Team>(TestData.Teams).Object);
            
            //Act
            var teamHandler = new GameApiService(dbContext.Object);
            var result = await teamHandler.GetAllTeamAsync();

            //Assert
            Assert.Equal(3, result.Count);
        }
    }
}