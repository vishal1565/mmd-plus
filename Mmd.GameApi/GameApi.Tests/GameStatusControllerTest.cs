using System.Linq;
using System;
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
        private readonly GameApiService _service;
        public GameStatusControllerTest()
        {
            var dbContext = new Mock<DataContext>();
            dbContext.SetupGet(x => x.Teams).Returns(TestFunctions.GetDbSet<Team>(TestData.Teams).Object);
            dbContext.SetupGet(x => x.Games).Returns(TestFunctions.GetDbSet<Game>(TestData.Games).Object);
            dbContext.SetupGet(x => x.Rounds).Returns(TestFunctions.GetDbSet<Round>(TestData.Rounds).Object);
            dbContext.SetupGet(x => x.Participants).Returns(TestFunctions.GetDbSet<Participant>(TestData.Participants).Object);
            dbContext.SetupGet(x => x.Guesses).Returns(TestFunctions.GetDbSet<Guess>(TestData.Guesses).Object);
            dbContext.SetupGet(x => x.Kills).Returns(TestFunctions.GetDbSet<Kill>(TestData.Kills).Object);
            dbContext.SetupGet(x => x.Scores).Returns(TestFunctions.GetDbSet<Score>(TestData.Scores).Object);
            dbContext.SetupGet(x => x.Requests).Returns(TestFunctions.GetDbSet<Request>(TestData.Requests).Object);

            _service = new GameApiService(dbContext.Object);
        }

        [Fact]
        public async void GetGameStatusTest()
        {
            //Act
            var result = await _service.GetAllTeamAsync();

            //Assert
            Assert.Equal(3, result.Count);
        }        
    }
}