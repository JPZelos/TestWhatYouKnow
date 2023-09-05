using System;
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
        private readonly ITestResultService _testResultService;
        private readonly IWorkContext _workContext;

        public AnswerController(IPermissionService permissionService, IChapterService chapterService, IQuestionService questionService, IAnswerService answerService, IRepository<Answer> answerRepository, ITestResultService testResultService, IWorkContext workContext) {
            _permissionService = permissionService;
            _chapterService = chapterService;
            _questionService = questionService;
            _answerService = answerService;
            _answerRepository = answerRepository;
            _testResultService = testResultService;
            _workContext = workContext;
        }

        // GET: Answer
        public ActionResult DoTest(int chapterId) {
            if (!_permissionService.Authorize("Answer.DoTest"))
                return RedirectToRoute("Login");

            var chapter = _chapterService.GetChapterById(chapterId);

            //TODO: need to move this in factory class
            var model = chapter.ToModel();

            return View(model.Questions.ToList());
        }

        [HttpPost]
        public ActionResult TestResult() {
            var form = ((System.Web.HttpRequestWrapper)Request).Form;

            var queryString = form.ToString();
            var queryPairs = queryString.Split('&');

            List<QuestionModel> questions = new List<QuestionModel>();

            List<TestResultModel> testResults = new List<TestResultModel>();

            foreach (var pair in queryPairs) {
                var questionId = int.Parse(pair.Split('=')[0]);
                var answeredValue = int.Parse(pair.Split('=')[1]);

                var question = _questionService.GetQuestionById(questionId);
                var answers = _answerRepository.Table.Where(x => x.Question.Id == questionId);
                var answered = answers.FirstOrDefault(x => x.Value == answeredValue);

                var testResult = new TestResultModel {
                    Score = question.Score
                };
                if (answered != null) {
                    testResult.AnswerId = answered.Id;
                }
                testResult.CustomerId = _workContext.CurrentCustomer.Id;
                testResult.Answer = answered.ToModel();

                testResults.Add(testResult);
                var qModel = question.ToModel();
                qModel.IsSuccess = question.SuccessValue == answered?.Value;

                questions.Add(qModel);
            }

            var model = new TestResults {
                Questions = questions,
                Results = testResults
            };

            return View(model);
        }
    }
}