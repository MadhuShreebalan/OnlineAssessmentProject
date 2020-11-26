//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Net.Mail;

//namespace UnitTestMail
//{
//    public interface ISmtpClient
//    {
//        bool Send(MailMessage mailMessage);
//    }
//    public class SmtpClientWrapper : ISmtpClient
//    {

//        public SmtpClient SmtpClient { get; set; }
//        public SmtpClientWrapper(string host, int port)
//        {
//            SmtpClient = new SmtpClient(host, port);
//        }

//        private bool Send(MailMessage mailMessage)
//        {
//            return Send(mailMessage);
//        }

//        bool ISmtpClient.Send(MailMessage mailMessage)
//        {
//            return true;
//        }
//    }
//    [TestClass]
//    public class SmtpProvider : ISmtpClient
//    {
//        private readonly ISmtpClient @object;

//        public SmtpProvider(ISmtpClient @object)
//        {
//            this.@object = @object;
//        }
//        [TestMethod]
//        public bool Send(MailMessage mailMessage)
//        {
//            return true;
//        }

//    }

//}
