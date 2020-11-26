using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Mail;


namespace OnlineAssessmentApplication.ServiceLayer
{
    /// <summary>
    /// To Send Email Notification. Credentials are configured at web config.
    /// </summary>
    public interface ISmtpClient
    {
        bool Send(MailMessage mailMessage);
    }
    [TestClass]
    public class INotificationService : ISmtpClient
    {
        private ISmtpClient @object;

        
        public INotificationService(ISmtpClient @object)
        {
            this.@object = @object;
        }

        [TestMethod]
        public bool Send( MailMessage mailMessage)
        {
            mailMessage = new MailMessage("madhushreebalan@gmail.com", "madhushreen2020@srishakthi.ac.in")
            {
                Subject = "Regarding Test Approval",
                Body = " Test is Scheduled. Kindly, provide your status"
            };
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (SmtpException smptNameNotFound)
            {
                Console.WriteLine("Server hostname is invalid:" + smptNameNotFound.Message);
                if (smptNameNotFound.InnerException != null)
                {
                    Console.WriteLine(smptNameNotFound.InnerException.Message);
                }
            }
            return true;
    }
    }
}
