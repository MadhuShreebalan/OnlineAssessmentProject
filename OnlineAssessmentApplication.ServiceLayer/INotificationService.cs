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
        void Send();
    }
    [TestClass]
    public class INotificationService : ISmtpClient
    {
        [TestMethod]
        public void Send()
        {
            MailMessage mailMessage = new MailMessage("madhushreebalan@gmail.com", "madhushreen2020@srishakthi.ac.in");
            mailMessage.Subject = "Regarding Test Approval";
            mailMessage.Body = " Test is Scheduled. Kindly, provide your status";
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
        }
    }
}
