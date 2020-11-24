using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using OnlineAssessmentApplication.ServiceLayer;


namespace UnitTestMail
{
    //public class MailTest
    //{
    //   public Mock INotificationService;

    //}
    [TestClass]
    public class UnitTest1
    {
        [SetUp]

        public void SetUp()
        {
             var mockINotificationService = new Mock<INotificationService>();
            var send = MethodToTest(mockINotificationService.Object);
        }

        private object MethodToTest(INotificationService @object)
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public void TestMethod1()
        {
           
        }
    }
}
