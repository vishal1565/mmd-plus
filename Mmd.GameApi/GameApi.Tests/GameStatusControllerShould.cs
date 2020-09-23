using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Castle.Core.Logging;
using DataAccess.Data;
using DataAccess.Data.Services;
using DataAccess.Model;
using DataAccess.Model.SharedModels;
using FluentAssertions;
using GameApi.Service.Controllers;
using GameApi.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GameApi.Tests
{
    public class GameStatusControllerShould
    {
        private readonly GameStatusController controller;

        public GameStatusControllerShould()
        {
            var dataContext = new Mock<DataContext>();
            var gameApiService = new Mock<GameApiService>(dataContext.Object);
            var requestContext = new RequestContext();
            var requestLoggingService = new RequestLoggingService(requestContext);
            var logger = Mock.Of<ILogger<GameStatusController>>();
            controller = new GameStatusController(requestContext, gameApiService.Object, requestLoggingService, logger);
        }
        [Fact]
        public void BeAControllerBase()
        {
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
            var hasAHttpGetMethod = TestFunctions.MethodHasAttribute(() => controller.Get(), typeof( HttpGetAttribute));
            hasAHttpGetMethod.Should().BeTrue();
        }

        [Fact]
        public async Task ReturnGameStatusResponse()
        {
            var response = await controller.Get();
            var result = response.Value;
            result.Should().BeAssignableTo<GameStatusResponse>();
        }

        [Fact]
        public async Task HaveAValidRequestId()
        {
            var response = await controller.Get();
            var result = response.Value;
            result.Should().BeAssignableTo<GameStatusResponse>();
            ((GameStatusResponse)result).RequestId.Should().NotBe(Guid.Empty);
        }

        // GameStatusControllerResponseTests
        
        // 1. If no current game is found, return errorGame is not running, 200
        // 2. If current game is there, but no current phase is found, return error try after some time, 200
        // 3. If any exception occurs while getting gamestatus, return error Internal Server Error, 500
        // Note: GameId and RoundId should be null and not new Guid() in all above cases.

    }
}