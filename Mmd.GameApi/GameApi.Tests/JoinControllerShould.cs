using DataAccess.Data;
using DataAccess.Data.Services;
using DataAccess.Model.SharedModels;
using FluentAssertions;
using GameApi.Service.Controllers;
using GameApi.Tests.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameApi.Tests
{
    public class JoinControllerShould
    {
        private readonly JoinController controller;

        public JoinControllerShould()
        {
            var dataContext = new Mock<DataContext>();
            var requestContext = new RequestContext();
            var gameApiServiceLogger = new Mock<ILogger<GameApiService>>().Object;
            var gameApiService = new Mock<GameApiService>(dataContext.Object, gameApiServiceLogger, requestContext);
            var requestLoggingService = new RequestLoggingService(requestContext, dataContext.Object);
            var logger = Mock.Of<ILogger<JoinController>>();
            controller = new JoinController(requestContext, gameApiService.Object, requestLoggingService, logger);
        }

        private Mock<ILogger<T>> GetMockLogger<T>()
        {
            return new Mock<ILogger<T>>();
        }

        [Fact]
        public void BeAController()
        {
            Assert.IsAssignableFrom<Controller>(controller);
        }

        [Fact]
        public void HaveAnApiControllerAttribute()
        {
            Attribute.GetCustomAttributes(typeof(JoinController), typeof(ApiControllerAttribute)).Any().Should().BeTrue();
        }

        [Fact]
        public void ProduceApplicationJsonResponse()
        {
            var producesAttribute = (ProducesAttribute)Attribute.GetCustomAttribute(typeof(JoinController), typeof(ProducesAttribute));
            producesAttribute.Should().NotBeNull();
            producesAttribute.ContentTypes.Should().Contain("application/json");
        }

        [Fact]
        public void HaveAnAuthorizationAttributeForBasicAuth()
        {
            var authorizeAttribute = (AuthorizeAttribute)Attribute.GetCustomAttribute(typeof(JoinController), typeof(AuthorizeAttribute));
            authorizeAttribute.Should().NotBeNull();
            authorizeAttribute.AuthenticationSchemes.Should().Be("BasicAuth");
        }

        [Fact]
        public void HaveAnHttpPostMethod()
        {
            var hasAHttpPostMethod = TestFunctions.MethodHasAttribute(() => controller.Post(), typeof(HttpPostAttribute));
            hasAHttpPostMethod.Should().BeTrue();
        }

        [Fact]
        public async Task ReturnJoinResponse()
        {
            var response = await controller.Post();
            var result = response.Value;
            result.Should().BeAssignableTo<JoinResponse>();
        }

        [Fact]
        public async Task HaveAValidRequestId()
        {
            var response = await controller.Post();
            var result = response.Value;
            result.Should().BeAssignableTo<JoinResponse>();
            ((JoinResponse)result).RequestId.Should().NotBe(Guid.Empty);
        }
    }
}
