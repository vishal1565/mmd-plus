using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Data.Services;
using DataAccess.Model;
using FluentAssertions;
using GameApi.Service.Controllers;
using GameApi.Service.Models;
using GameApi.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameApi.Tests
{
    public class GameStatusControllerShould
    {
        [Fact]
        public void BeAControllerBase()
        {
            var dataContext = new Mock<DataContext>();
            var gameApiService = new Mock<GameApiService>(dataContext.Object);
            var requestContext = new RequestContext();
            var controller = new GameStatusController(requestContext, gameApiService.Object);
            Assert.IsAssignableFrom<ControllerBase>(controller);
        }

        [Fact]
        public void HaveAnApiControllerAttribute()
        {
            Attribute.GetCustomAttributes(typeof(GameStatusController), typeof(ApiControllerAttribute)).Any().Should().BeTrue();
        }

        [Fact]
        public void ProduceApplicationJsonResponse()
        {
            var producesAttribute = (ProducesAttribute)Attribute.GetCustomAttribute(typeof(GameStatusController), typeof(ProducesAttribute));
            producesAttribute.Should().NotBeNull();
            producesAttribute.ContentTypes.Should().Contain("application/json");
        }

        [Fact]
        public void HaveAnHttpGetMethod()
        {
            var dataContext = new Mock<DataContext>();
            var gameApiService = new Mock<GameApiService>(dataContext.Object);
            var requestContext = new RequestContext();
            var controller = new GameStatusController(requestContext, gameApiService.Object);
            var hasAHttpGetMethod = TestFunctions.MethodHasAttribute(() => controller.Get(), typeof( HttpGetAttribute));
            hasAHttpGetMethod.Should().BeTrue();
        }

        [Fact]
        public async void ReturnGameStatusResponse()
        {
            var dataContext = new Mock<DataContext>();     
            var gameApiService = new Mock<GameApiService>(dataContext.Object);
            var requestContext = new RequestContext();
            var controller = new GameStatusController(requestContext, gameApiService.Object);
            var response = await controller.Get();
            var result = (ActionResult<GameStatusResponse>)response.Result;
            var resultValue = ((JsonResult)result.Result).Value;
            resultValue.Should().BeAssignableTo<GameStatusResponse>();
        }

        [Fact]
        public async void HaveAValidRequestId()
        {
            var dataContext = new Mock<DataContext>();
            var gameApiService = new Mock<GameApiService>(dataContext.Object);
            var requestContext = new RequestContext();
            var controller = new GameStatusController(requestContext, gameApiService.Object);
            var response = await controller.Get();
            var result = (ActionResult<GameStatusResponse>)response.Result;
            var resultValue = ((JsonResult)result.Result).Value;
            resultValue.Should().BeAssignableTo<GameStatusResponse>();
            ((GameStatusResponse)resultValue).RequestId.Should().NotBe(new Guid());
        }

        // GameStatusControllerResponseTests
        
        // 1. If no current game is found, return errorGame is not running, 200
        [Fact]
        public async void ReturnGameNotRunning_GivenNoCurrentGameIsFound()
        {
            var dataContext = new Mock<DataContext>();
            dataContext.SetupGet(x => x.Phases).Returns(TestFunctions.GetDbSet(new List<Phase>().AsQueryable()).Object);
            var gameApiService = new Mock<GameApiService>(dataContext.Object);
            var requestContext = new RequestContext();
            var controller = new GameStatusController(requestContext, gameApiService.Object);
            var response = await controller.Get();
            var result = response.Result;
        }

        // 2. If current game is there, but no current phase is found, return error try after some time, 200
        // 3. If any exception occurs while getting gamestatus, return error Internal Server Error, 500
        // Note: GameId and RoundId should be null and not new Guid() in all above cases.

    }
}