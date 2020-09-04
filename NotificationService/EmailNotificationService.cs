using DataAccess.Model.SharedModels;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace NotificationService
{
    public class EmailNotificationService : INotificationService
    {
        private SmtpClient Client { get; set; }

        public EmailNotificationService(SmtpClient client)
        {
            Client = client;
        }

        public bool Notify(NotificationContent content)
        {
            if(string.IsNullOrEmpty(content.Sender)) throw new ArgumentNullException("Sender");
            if (content.Recievers == null || content.Recievers.Count == 0) throw new ArgumentNullException("Receivers");

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(content.Sender)
            };

            foreach (var item in content.CcUsers)
            {
                mail.CC.Add(new MailAddress(item));
            };

            foreach (var item in content.Recievers)
            {
                mail.To.Add(new MailAddress(item));
            };


            mail.Subject = content.Subject;
            mail.Body = content.Body;
            mail.IsBodyHtml = true;

            try
            {
                Client.Send(mail);
            }
            catch (SmtpFailedRecipientException)
            { 
            
            }
            return true;

        }
    }
}
