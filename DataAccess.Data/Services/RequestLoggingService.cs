using DataAccess.Data.Abstract;
using DataAccess.Model;
using DataAccess.Model.SharedModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Services
{
    public class RequestLoggingService : IRequestLoggingService
    {
        private readonly RequestContext requestContext;
        public RequestLoggingService(RequestContext requestContext)
        {
            this.requestContext = requestContext ?? throw new ArgumentNullException("RequestContext");
        }
        public Task RecordRequest(RequestMethod method, int? statusCode, RequestApi requestApi)
        {
            throw new NotImplementedException();
        }
    }
}
