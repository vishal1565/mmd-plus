using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;


namespace testMail
{
    public class SimpleAsynchronousExample
    {
        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }
        public static void Main(string[] args)
        {
            // Command-line argument must be the SMTP host.
            SmtpClient client = new SmtpClient("smtp.mailgun.org");
            //client.Host = "smtp.mail.org";
            client.Port = 587;
            NetworkCredential NC = new NetworkCredential();
            NC.UserName = "postmaster@sandbox3b061363637248538d0180df369c297b.mailgun.org";
            NC.Password = "94a85011864a1f81c87168baba09bee4-d5e69b0b-7dc3a05b";
            client.Credentials = NC;
            
            MailAddress from = new MailAddress("meghraj-manikrao.jagtap@db.com",
               "Jane " + (char)0xD8 + " Clayton",
            System.Text.Encoding.UTF8);
          
            MailAddress to = new MailAddress("itsmemeghraj@gmail.com");
          
            MailMessage message = new MailMessage(from, to);
            message.Body = "This is a test email message sent by an application. ";
            
            
            message.Subject = "test message 1";
            
            client.Send(message);
            
            Console.WriteLine("Goodbye.");
        }
    }
}