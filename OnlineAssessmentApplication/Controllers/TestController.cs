using OnlineAssessmentApplication.Filters;
using OnlineAssessmentApplication.ServiceLayer;
using OnlineAssessmentApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineAssessmentApplication.Controllers
{
    [AuthenticationFilter]
    //[StudentAuthorizeFilter]
    //[LogCustomExceptionFilter]
    public class TestController : Controller
    {
        readonly ITestService testService;
        readonly QuestionService questionService;
        readonly AnswerService answerService;
        readonly INotificationService notificationService;
        public TestController(ITestService testService, QuestionService questionService, AnswerService answerService, INotificationService notificationService)
        {
            this.testService = testService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.notificationService = notificationService;
        }
        public TestController()
        {
        }
        // GET: Test
        public ActionResult CreateTest()
        {

            return View();
        }
        [HttpPost]
        [ActionName("CreateTest")]
        public ActionResult SaveTest(CreateTestViewModel newTest)//Create Test
        {
            int testId = 0;
            if (ModelState.IsValid)
            {
                newTest.UserId = Convert.ToInt32(Session["CurrentUserID"]);
                newTest.CreatedBy = newTest.UserId;
                newTest.CreatedTime = DateTime.Now;
                testId= testService.CreateNewTest(newTest);
                TempData["TestId"] = testId;

                return RedirectToAction("CreateQuestions","Question");
            }
            return View();
        }
        public ActionResult EditTest(int testId)
        {

            TestViewModel test = testService.GetTestByTestId(testId);
            return View(test);
        }
        [HttpPost]
        public ActionResult EditTest(EditTestViewModel editedData)
        {
            if (ModelState.IsValid)
            {
                editedData.ModifiedBy = Convert.ToInt32(Session["CurrentUserID"]);
                editedData.ModifiedTime = DateTime.Now;
                testService.UpdateTest(editedData);
                return RedirectToAction("UpcomingTest");
            }
            return View();

        }
        public ActionResult DeleteTest(int testId)
        {

            testService.DeleteTest(testId);
            return RedirectToAction("UpcomingTest");
        }

        [HttpGet]
        public ActionResult GetApproval()
        {
            notificationService.Send();
            return RedirectToAction("UpcomingTest");
        }
        [HttpGet]
        public ActionResult UpcomingTest(FilterPanel filterPanel)
        {
            IEnumerable<TestViewModel> test = testService.DisplayAvailableTestDetails(filterPanel);
            return View(test);
        }

        public ActionResult VerifyPasscode(int passcode)
        {
            if (testService.VerifyPasscode(passcode))
                return RedirectToAction("TakeTest");
            else
            {
                TempData["Passcode_Alert"] = "Wrong Passcode.Please enter the correct passcode";
                return RedirectToAction("UpcomingTest");
            }
        }

        public ActionResult ViewScore(ResultViewModel resultViewModel)
        {
            IEnumerable<ResultViewModel> resultViewModels = testService.CalculateScore(resultViewModel);
            return View(resultViewModels);
        }
        public ActionResult UpdateAcceptStatus(int testId)
        {
            testService.UpdateAcceptStatus(testId);
            TempData["message"] = "Test Accepted Successfully";
            return RedirectToAction("index","dashboard");
        }
        public ActionResult UpdateRejectStatus(int testId)
        {
            testService.UpdateRejectStatus(testId);
            TempData["message"] = "Test Rejected Successfully";
            return RedirectToAction("index", "dashboard");
        }
        public ActionResult TakeTest()
        {
            int testId = 3;
            int questionId = 5;
            ViewData["questions"] = questionService.DisplayAllDetails(testId);
            ViewData["answers"] = answerService.DisplayAnswers(questionId);
            //  IEnumerable< QuestionsViewModel> qns = questionServices.DisplayAllDetails(testId);
            // IEnumerable<AnswerViewModel> ans = answerService.DisplayAnswers(questionId);
            return View();
        }
        public ActionResult DisplayScoresTeacher(int grade)
        {
            IEnumerable<ResultViewModel> resultViewModels = testService.DisplayScoresTeacher(grade);
            return View(resultViewModels);
        }

        //Student
        public ActionResult StudentScore(int userId, int testId)
        {
            IEnumerable<ResultViewModel> resultViewModels = testService.StudentScore(userId, testId);
            return View(resultViewModels);
        }
    }
}