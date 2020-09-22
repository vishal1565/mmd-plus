using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using GameApi.Service.Controllers;
using GameApi.Service.Models;
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
        public void HaveAHttpGetMethod()
        {
            var controller = new GameStatusController();
            var hasAHttpGetMethod = MethodHasAttribute(() => controller.Get(), typeof( HttpGetAttribute));
            hasAHttpGetMethod.Should().BeTrue();
        }

        [Fact]
        public async void ShouldReturnGameStatusResponse()
        {
            var controller = new GameStatusController();
            var response = await controller.Get();
            var result = (OkObjectResult)response.Result;
            result.Should().NotBeNull();
            result.Value.Should().BeAssignableTo<GameStatusResponse>();
        }

        [Fact]
        public async void ShouldHaveAValidRequestId()
        {
            var controller = new GameStatusController();
            var response = await controller.Get();
            var result = (OkObjectResult)response.Result;
            result.Value.Should().BeAssignableTo<GameStatusResponse>();
            ((GameStatusResponse)result.Value).RequestId.Should().NotBe(new Guid());
        }

        public static MethodInfo MethodOf( Expression<System.Action> expression )
        {
            MethodCallExpression body = (MethodCallExpression)expression.Body;
            return body.Method;
        }

        public static bool MethodHasAttribute( Expression<System.Action> expression , Type attributeType)
        {
            var methodInfo = MethodOf( expression );

            const bool includeInherited = false;
            return methodInfo.GetCustomAttributes( attributeType, includeInherited ).Any();
        }
    }
}