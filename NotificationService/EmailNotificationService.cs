using DataAccess.Model.SharedModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService
{
    public class EmailNotificationService : INotificationService
    {
        public bool Notify(NotificationContent content)
        {
            return true;
        }
    }
}
