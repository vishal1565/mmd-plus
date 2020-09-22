using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using GameApi.Service.Controllers;
using GameApi.Service.Models;
using GameApi.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GameApi.Tests
{
    public class GameStatusControllerShould
    {
        [Fact]
        public void BeAControllerBase()
        {
            var controller = new GameStatusController();
            Assert.IsAssignableFrom<ControllerBase>(controller);
        }

        [Fact]
        public void HaveAnApiControllerAttribute()
        {
            Attribute.GetCustomAttributes(typeof(GameStatusController), typeof(ApiControllerAttribute)).Any().Should().BeTrue();
        }

        [Fact]
        public void HaveAnHttpGetMethod()
        {
            var controller = new GameStatusController();
            var hasAHttpGetMethod = TestFunctions.MethodHasAttribute(() => controller.Get(), typeof( HttpGetAttribute));
            hasAHttpGetMethod.Should().BeTrue();
        }

        [Fact]
        public async void ReturnGameStatusResponse()
        {
            var controller = new GameStatusController();
            var response = await controller.Get();
            var result = (OkObjectResult)response.Result;
            result.Should().NotBeNull();
            result.Value.Should().BeAssignableTo<GameStatusResponseData>();
        }

        [Fact]
        public async void HaveAValidRequestId()
        {
            var controller = new GameStatusController();
            var response = await controller.Get();
            var result = (OkObjectResult)response.Result;
            result.Value.Should().BeAssignableTo<GameStatusResponse>();
            ((GameStatusResponse)result.Value).RequestId.Should().NotBe(new Guid());
        }
    }
}