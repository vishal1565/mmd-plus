using DataAccess.Data.Abstract;
using DataAccess.Model;
using DataAccess.Model.SharedModels;
using GameApi.Service.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameApi.Service.Middleware
{
    public class ThrottlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly long ticksPerSecond = 10000000;

        public ThrottlingMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate ?? throw new ArgumentNullException("requestDelegate");
        }

        public async Task InvokeAsync(HttpContext httpContext, IGameApiService gameApiService, RequestContext requestContext, ILogger<ThrottlingMiddleware> logger, IRequestLoggingService requestLoggingService)
        {
            var endpoint = httpContext.GetEndpoint();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            if (endpoint != null)
            {
                var metadata = endpoint.Metadata;

                foreach(var metadataObject in metadata)
                {
                    if (metadataObject.GetType().Equals(typeof(ThrottleAttribute)))
                    {
                        var throttleAttribute = (ThrottleAttribute)metadataObject;
                        var path = httpContext.Request.Path;
                        var username = httpContext.User.Identity.Name;
                        if (username == null || !path.HasValue || !(await gameApiService.ValidRequest(path.ToString(), username, DateTime.UtcNow)))
                        {
                            logger.LogWarning($"Request { requestContext.RequestId } rejected in api throttling");

                            //await RecordRequest(httpContext, requestLoggingService, logger, requestContext);
                            
                            string json = JsonConvert.SerializeObject(new GuessResponse
                            {
                                RequestId = requestContext.RequestId,
                                Err = new Error
                                {
                                    Message = "Rejected in Api Throttling",
                                    Description = $"Please send a valid request. Only 1 request allowed every { throttleAttribute.ticks / ticksPerSecond } seconds"
                                }
                            }, jsonSerializerSettings);

                            httpContext.Response.StatusCode = 400;
                            httpContext.Response.ContentType = "application/json; charset=utf-8";
                            
                            await httpContext.Response.WriteAsync(json, Encoding.UTF8);
                            
                            return;
                        }
                    }
                }
            }

            await _next(httpContext);
        }
        
        private async Task RecordRequest(HttpContext httpContext, IRequestLoggingService requestLoggingService, ILogger<ThrottlingMiddleware> logger, RequestContext requestContext)
        {
            try
            {
                var requestMethod = (RequestMethod)Enum.Parse(typeof(RequestMethod), httpContext.Request.Method);

                httpContext.Request.Path.StartsWithSegments("/api/", out PathString apiString);

                var requestApi = (RequestApi)Enum.Parse(typeof(RequestApi), apiString);

                await requestLoggingService.RecordRequest(requestMethod, 400, requestApi);
            }
            catch(Exception)
            {
                logger.LogError($"Failed to record request {requestContext.RequestId}");
            }
        }
    }
}
