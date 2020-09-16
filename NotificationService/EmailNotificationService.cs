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

            //if (string.IsNullOrEmpty(content.Sender)) throw new ArgumentNullException("Sender");  //Uncomment these lines in final code
            //if (content.Recievers == null || content.Recievers.Count == 0) throw new ArgumentNullException("Receivers");

            MailMessage mail = new MailMessage
            {
                From = new MailAddress("meghraj-manikrao.jagtap@db.com")
                //From = new MailAddress(content.Sender)
            };

            //foreach (var item in content.CcUsers)
            //{
            //    mail.CC.Add(new MailAddress(item));
            //};

            //foreach (var item in content.Recievers)
            //{
            //    mail.To.Add(new MailAddress(item));
            //};

            //temporary code till we get domain
            mail.To.Add(new MailAddress("itsmemeghraj@gmail.com"));
            mail.Subject = "Hi from inside codecomp3";//content.Subject;
            mail.Body = content.Body;
            mail.IsBodyHtml = true;

            try
            {
                Client.Send(mail);
                Console.WriteLine(".....Mail sent....");
            }
            catch (SmtpFailedRecipientException e)
            {
                Console.WriteLine(e);
            }
            return true;

        }
    }
}