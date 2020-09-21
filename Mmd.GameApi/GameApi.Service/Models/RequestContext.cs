using System;
namespace GameApi.Service.Models
{
    public class RequestContext
    {
        public RequestContext()
        {
            RequestId = Guid.NewGuid();
        }
        public Guid RequestId { get; set; }
    }
}