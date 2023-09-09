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
using TWYK.Services.Quizzes;
using TWYK.Services.Security;
using TWYK.Services.TestResults;
using TWYK.Services.Topics;
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
        private readonly IQuizService _quizService;
        private readonly ITopicService _topicService;

        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Chapter> _chapteRepository;

        private readonly IRepository<TestResult> _testResultRepository;

        private readonly ITestResultService _testResultService;
        private readonly IWorkContext _workContext;

        public AnswerController(IPermissionService permissionService, IChapterService chapterService, IQuestionService questionService, IAnswerService answerService, IQuizService quizService, ITopicService topicService, IRepository<Answer> answerRepository, IRepository<Chapter> chapteRepository, IRepository<TestResult> testResultRepository, ITestResultService testResultService, IWorkContext workContext) {
            _permissionService = permissionService;
            _chapterService = chapterService;
            _questionService = questionService;
            _answerService = answerService;
            _quizService = quizService;
            _topicService = topicService;
            _answerRepository = answerRepository;
            _chapteRepository = chapteRepository;
            _testResultRepository = testResultRepository;
            _testResultService = testResultService;
            _workContext = workContext;
        }

        public ActionResult Results() {
            if (!_permissionService.Authorize("Answer.Results")) {
                return RedirectToRoute("Login");
            }
            var model = new ResultModel();
            var user = _workContext.CurrentCustomer;
            var userQuizzes = _quizService.GetAllUserQuizs(user.Id);
            var topicIds = userQuizzes.Select(q => q.Chapter.TopicId).Distinct().ToList();

            //List<Topic> userTopics = new List<Topic>();
            //foreach (var topicId in topicIds) {
            //    var topic = new Topic
            //    userTopics.Add();
            //}




            model.FullName = user.FirstName + " " + user.LastName;

            

            var modelTopics = new List<TopicModel>();

            foreach (var topicId in topicIds) {
                var topic = _topicService.GetTopicById(topicId).ToModel();
                var chapterIds = userQuizzes.Select(q => q.ChapterId).Distinct().ToList();
                foreach (var chapterId in chapterIds)
                {
                    var chapterModel = _chapterService.GetChapterById(chapterId).ToModel();
                    chapterModel.Quizzes = userQuizzes.Where(q => q.ChapterId == chapterId).ToList();
                    topic.Chapters.Add(chapterModel);
                }

                modelTopics.Add(topic);

                //var chapterIds = quizzes.Select(q => q.ChapterId).Distinct().ToList();
                //foreach (var chapterId in chapterIds)
                //{
                //    var chapterModel = _chapterService.GetChapterById(chapterId).ToModel();
                //    chapterModel.Quizzes = quizzes.Where(q => q.ChapterId == chapterId).ToList();
                //    model.Chapters.Add(chapterModel);
                //}

            }
            model.Topics = modelTopics;

            



            return View(model);
        }

        // GET: Answer
        public ActionResult DoTest(int chapterId) {
            if (!_permissionService.Authorize("Answer.DoTest")) {
                return RedirectToRoute("Login");
            }

            var chapter = _chapterService.GetChapterById(chapterId);

            var userId = _workContext.CurrentCustomer.Id;
            var tries = _quizService.GetMaxQuizTries(userId, chapterId);

            Quiz quiz = new Quiz {
                CustomerId = userId,
                ChapterId = chapterId,
                Score = 0,
                Tries = tries,
                Success = false
            };
            _quizService.InsertQuiz(quiz);

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
            var quiz = _quizService.GetQuizById(int.Parse(quizId));

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
                    var chapter = _chapterService.GetChapterById(question.ChapterId);

                    if (answered != null) {
                        // Declare view model
                        var testResult = new TestResult {
                            Score = question.SuccessValue == answered.Value ? question.Score : 0,
                            AnswerId = answered.Id,
                            CustomerId = _workContext.CurrentCustomer.Id,
                            Success = question.SuccessValue == answered.Value,
                            QuizId = int.Parse(quizId)
                        };

                        quiz.Score += question.SuccessValue == answered.Value ? question.Score : 0;
                        quiz.Success = quiz.Score >= chapter.PasScore;
                        _quizService.UpdateQuiz(quiz);

                        _testResultRepository.Insert(testResult);
                    }
                    else {
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