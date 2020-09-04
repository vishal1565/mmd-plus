using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model.SharedModels
{
    public class NotificationContent
    {
        public List<string> Senders { get; set; }
        public List<string> Recievers { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CcUsers { get; set; }
        public string BccUsers { get; set; }
    }
}
