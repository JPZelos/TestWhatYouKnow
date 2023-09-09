using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TWYK.Core;
using TWYK.Core.Data;
using TWYK.Core.Domain;
using TWYK.Services.Answers;
using TWYK.Services.Chapters;
using TWYK.Services.Questions;
using TWYK.Services.Security;
using TWYK.Services.TestResults;
using TWYK.Web.Infrastructure.Mapper;
using TWYK.Web.Models;

namespace TWYK.Web.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IChapterService _chapterService;
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;

        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Chapter> _chapteRepository;
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<TestResult> _testResultRepository;

        private readonly ITestResultService _testResultService;
        private readonly IWorkContext _workContext;

        public AnswerController(IPermissionService permissionService, IChapterService chapterService, IQuestionService questionService, IAnswerService answerService, IRepository<Answer> answerRepository, IRepository<Chapter> chapteRepository, IRepository<Quiz> quizRepository, IRepository<TestResult> testResultRepository, ITestResultService testResultService, IWorkContext workContext) {
            _permissionService = permissionService;
            _chapterService = chapterService;
            _questionService = questionService;
            _answerService = answerService;
            _answerRepository = answerRepository;
            _chapteRepository = chapteRepository;
            _quizRepository = quizRepository;
            _testResultRepository = testResultRepository;
            _testResultService = testResultService;
            _workContext = workContext;
        }

        // GET: Answer
        public ActionResult DoTest(int chapterId) {
            if (!_permissionService.Authorize("Answer.DoTest")) {
                return RedirectToRoute("Login");
            }

            var chapter = _chapterService.GetChapterById(chapterId);

            var userId = _workContext.CurrentCustomer.Id;
            var userChapterQuizzes = _quizRepository.Table.Where(q=>q.CustomerId==userId && q.ChapterId==chapterId).ToList();
            var tries = userChapterQuizzes.Any() ? userChapterQuizzes.Max(q => q.Tries) + 1 : 1;
            
            Quiz quiz = new Quiz {
                CustomerId = userId,
                ChapterId = chapterId,
                Score = 0,
                Tries = tries,
                Success = false
            };
            _quizRepository.Insert(quiz);
            
            var model = chapter.ToModel();
            model.QuizId = quiz.Id;

            return View(model);
        }

        [HttpPost]
        public ActionResult TestResult() {
            var form = ((HttpRequestWrapper)Request).Form;

            var queryString = form.ToString();
            var queryPairs = queryString.Split('&');

            List<QuestionModel> questions = new List<QuestionModel>();
            List<TestResultModel> testResults = new List<TestResultModel>();

            bool success = true;
            string message = "Success";

            var quizId = queryPairs[0].Split('=')[1];

            foreach (var pair in queryPairs) {
                var isQuiz = pair.Split('=')[0] == "QuizId";

                if (!isQuiz) {
                    // Get question Id and User Answwe Value
                    var questionId = int.Parse(pair.Split('=')[0]);
                    var answeredValue = int.Parse(pair.Split('=')[1]);

                    // Get question and answered answer records from db
                    var question = _questionService.GetQuestionById(questionId);
                    var answers = _answerRepository.Table.Where(x => x.Question.Id == questionId);
                    var answered = answers.FirstOrDefault(x => x.Value == answeredValue);

                    if (answered != null)
                    {
                        // Declare view model
                        var testResult = new TestResult
                        {
                            Score = question.SuccessValue == answered.Value ? question.Score : 0,
                            AnswerId = answered.Id,
                            CustomerId = _workContext.CurrentCustomer.Id,
                            Success = question.SuccessValue == answered.Value,
                            QuizId = int.Parse(quizId)
                        };
                        _testResultRepository.Insert(testResult);
                    }
                    else
                    {
                        success = false;
                        message = "An error occured";
                        Response.StatusCode = 500;
                        break;
                    }
                }
                
            }

            return Json(new { success, responseText = message }, JsonRequestBehavior.AllowGet);
        }
    }
}