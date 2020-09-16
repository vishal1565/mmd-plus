using DataAccess.Model.SharedModels;
using System;
using System.IO;
using System;
using System.IO;
using RestSharp;
using RestSharp.Authenticators;

namespace NotificationService
{
    public class EmailNotificationService : INotificationService
    {
        public bool Notify(NotificationContent content)
        {
            Console.WriteLine(SendSimpleMessage().Content.ToString());
            return true;

        }
        public static IRestResponse SendSimpleMessage()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            "386341fda5ba13481a4317a9945ad48d-0f472795-8f637e50");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox3b061363637248538d0180df369c297b.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <meghraj-manikrao.jagtap@db.com>");
            request.AddParameter("to", "itsmemeghraj@gmail.com");
            //request.AddParameter("to", "YOU@YOUR_DOMAIN_NAME");
            request.AddParameter("subject", "Hello");
            request.AddParameter("text", "Testing some Mailgun awesomness!");
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
