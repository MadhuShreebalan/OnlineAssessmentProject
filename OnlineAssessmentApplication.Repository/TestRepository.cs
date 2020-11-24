using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.ViewModel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineAssessmentApplication.Repository
{
    public interface ITestRepository
    {
        IEnumerable<Test> DisplayAvailableTestDetails(FilterPanel filterPanel);
        IEnumerable<ResultViewModel> CalculateScore(ResultViewModel resultViewModel);
        IEnumerable<ResultViewModel> DisplayScoresTeacher(int grade);
        IEnumerable<ResultViewModel> StudentScore(int userId, int testId);
        bool VerifyPasscode(int passcode);
        int CreateNewTest(Test test);
        Test GetTestByTestId(int testId);
        void UpdateTest(Test editTest);
        void DeleteTest(int testId);
        void UpdateAcceptStatus(int testId);
        void UpdateRejectStatus(int testId);
    }
    public class TestRepository : ITestRepository
    {
        readonly AssessmentDbContext db;
        public TestRepository()
        {
            db = new AssessmentDbContext();
        }
        public int CreateNewTest(Test test) //To create new test
        {

            db.Tests.Add(test);
            db.SaveChanges();
            int testId = test.TestId;
            return testId;

        }

        public void UpdateTest(Test editTest)
        {

            Test test = db.Tests.Find(editTest.TestId);
            if (test != null)
            {
                test.TestId = editTest.TestId;
                test.TestName = editTest.TestName;
                test.TestDate = editTest.TestDate;
                test.StartTime = editTest.StartTime;
                test.EndTime = editTest.EndTime;
                test.Grade = editTest.Grade;
                test.Subject = editTest.Subject;

                test.ModifiedTime = editTest.ModifiedTime;
                db.SaveChanges();
            }

        }
        public void DeleteTest(int testId)
        {
            db.Tests.Remove(GetTestByTestId(testId));
            db.SaveChanges();
        }
        public Test GetTestByTestId(int testId)
        {
            return db.Tests.Find(testId);
        }

        readonly string userName = HttpContext.Current.User.Identity.Name.ToString();

        public IEnumerable<Test> DisplayAvailableTestDetails(FilterPanel filterPanel)
        {
            using (AssessmentDbContext AssessmentDBContext = new AssessmentDbContext())
            {
                if (HttpContext.Current.User.IsInRole("Student"))
                {
                    User currentUser = AssessmentDBContext.Users.FirstOrDefault(user => user.Name == userName);
                    IEnumerable<Test> tests = AssessmentDBContext.Tests.Where(test => (test.Grade == currentUser.UserGrade && test.Status.Equals("Accepted")) && (test.Subject.Equals(filterPanel.SubjectId) || filterPanel.SubjectId == 0) && (test.TestName.Contains(filterPanel.SearchBy) || filterPanel.SearchBy == null)).ToList();
                    return tests;
                }
                else if (HttpContext.Current.User.IsInRole("Principal") || HttpContext.Current.User.IsInRole("Teacher"))
                {
                    IEnumerable<Test> tests = AssessmentDBContext.Tests.Where(test => (test.Subject.Equals(filterPanel.SubjectId) || filterPanel.SubjectId == 0) && (test.Grade.Equals(filterPanel.GradeId) || filterPanel.GradeId == 0) && (test.TestName.Contains(filterPanel.SearchBy) || filterPanel.SearchBy == null)).ToList();
                    return tests;
                }
                else
                {
                    return AssessmentDBContext.Tests.ToList();
                }
            }
        }



            public bool VerifyPasscode(int passcode)
        {
            using (AssessmentDbContext AssessmentDBContext = new AssessmentDbContext())
            {
                Test verifiedTest = AssessmentDBContext.Tests.Where(test => test.Passcode == passcode).FirstOrDefault();
                if (verifiedTest != null)
                    return true;
                else
                    return false;
            }

        }

        public IEnumerable<ResultViewModel> CalculateScore(ResultViewModel resultViewModel)
        {
            using (AssessmentDbContext AssessmentDBContext = new AssessmentDbContext())
            {
                User currentUser = AssessmentDBContext.Users.FirstOrDefault(user => user.Name == userName);
                SqlParameter userId = new SqlParameter("@UserId", currentUser.UserId);

                var resultViewModels = AssessmentDBContext.Database.SqlQuery<ResultViewModel>("SP_CalculateScore @UserId", userId).ToList();

                return resultViewModels;
            }
        }

        public void UpdateAcceptStatus(int testId)
        {
            using (AssessmentDbContext AssessmentDBContext = new AssessmentDbContext())
            {
                Test test = AssessmentDBContext.Tests.Find(testId);
                test.Status = "Accepted";
                AssessmentDBContext.SaveChanges();
            }
        }

        public void UpdateRejectStatus(int testId)
        {
            using (AssessmentDbContext AssessmentDBContext = new AssessmentDbContext())
            {
                Test test = AssessmentDBContext.Tests.Find(testId);
                test.Status = "Rejected";
                AssessmentDBContext.SaveChanges();
            }
        }

        public IEnumerable<ResultViewModel> DisplayScoresTeacher(int grade)
        {
            using (AssessmentDbContext assessmentDbContext = new AssessmentDbContext())
            {
                if (grade == 0)
                {

                    var result = assessmentDbContext.Database.SqlQuery<ResultViewModel>("DefaultScores").ToList();
                    return result;
                }
                else
                {
                    SqlParameter Grade = new SqlParameter("@Grade", grade);
                    var resultViewModels = assessmentDbContext.Database.SqlQuery<ResultViewModel>("Allscores @Grade", Grade).ToList();
                    foreach (var r in resultViewModels)
                    {
                        var stu = assessmentDbContext.Users.Where(user => user.UserId == r.UserId).FirstOrDefault();
                        r.StudentName = stu.Name;
                    }
                    return resultViewModels;
                }

            }
        }

            public IEnumerable<ResultViewModel> StudentScore(int userId, int testId)
            {
                using (AssessmentDbContext assessmentDbContext = new AssessmentDbContext())
                {

                    SqlParameter UserId = new SqlParameter("@UserId", userId);
                    SqlParameter TestId = new SqlParameter("@TestId", testId);
                    var result = assessmentDbContext.Database.SqlQuery<ResultViewModel>("calculation @UserId ,@TestId", UserId, TestId).ToList();
                    return result;
                }
            }

        }
        
    }
