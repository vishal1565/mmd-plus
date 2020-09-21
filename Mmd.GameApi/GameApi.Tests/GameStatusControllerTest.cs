using System.Net.Mime;
using System.Linq;
using System;
using DataAccess.Data;
using DataAccess.Data.Services;
using DataAccess.Model;
using GameApi.Tests.Helpers;
using Moq;
using Xunit;
using GameApi.Service.Controllers;
using GameApi.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameApi.Tests
{
    public class GameStatusControllerTest
    {
        private readonly Mock<GameApiService> _service;
        private readonly GameStatusController _controller;

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

            _service = new Mock<GameApiService>(dbContext.Object){ CallBase = true };
            _controller = new Mock<GameStatusController>(_service.Object){ CallBase = true }.Object;
        }

        // It should be an async function call for Index() method
        [Fact]
        public async Task Index_IsAsync()
        {
            // Act
            var result = _controller.Index();

            // Assert
            await Assert.IsAssignableFrom<Task>(result);
            await Assert.IsType<Task<ActionResult<GameStatusResponse>>>(result);
        }

        // It should contain a valid requestId
        [Fact]
        public async Task Index_DoesContainRequestId()
        {
            var result = await _controller.Index();

            Assert.NotNull(result.Result);
            var gameStatus = (GameStatusResponse)((OkObjectResult)result.Result).Value;
            Assert.NotNull(gameStatus.RequestId);
            Assert.NotEqual(new Guid().ToString(), gameStatus.RequestId.ToString());
        }

        // It should contain all the participants in TeamData format for current game
        // On any exception, it should add appropriate message to err field
        // If no current game is found,      
        // It should return a json object of type GameStatusResponse
    }
}