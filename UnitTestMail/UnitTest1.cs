using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineAssessmentApplication.Controllers;
using OnlineAssessmentApplication.ServiceLayer;
using System.Net.Mail;


namespace UnitTestMail
{
    [TestClass]
    public class INotificationServiceTest
    {
        private readonly TestController testController;
        private readonly Mock<ISmtpClient> mockISmtpService = new Mock<ISmtpClient>();
        private readonly  Mock<INotificationService> mockINotificationService = new Mock<INotificationService>();
        private readonly Mock<AnswerService> mockanswer = new Mock<AnswerService>();
        private readonly Mock<QuestionService> mockquestion = new Mock<QuestionService>();
        private readonly Mock<ITestService> mocktest = new Mock<ITestService>();

        public INotificationServiceTest()
        {

           // testController = new TestController(mocktest.Object, mockquestion.Object, mockanswer.Object, mockINotificationService.Object);
        }
        [TestMethod]
        public void Send()
        {
           INotificationService smtpProvider = new INotificationService(mockISmtpService.Object);
           MailMessage mailMessage = new MailMessage ("madhushreebalan@gmail.com", "madhushreen2020@srishakthi.ac.in");

            //string @from = "from@from.com";
            //string to = "to@to.com";
            bool send = smtpProvider.Send(mailMessage);
            Assert.IsTrue(send);

            //mailMessage = new MailMessage("madhushreebalan@gmail.com", "madhushreen2020@srishakthi.ac.in", "Regarding Test Approval", " Test is Scheduled. Kindly, provide your status");
            //testController.GetApproval(mailMessage);
            //mockISmtpService.Setup(x => x.Send(It.IsAny<string>()).Returns(true);
            //mockISmtpService.Verify(t => t.Send(mailMessage), Times.Once);
            //Assert.AreEqual("UpcomingTest", mockISmtpService.Object);
        }
    }
}

