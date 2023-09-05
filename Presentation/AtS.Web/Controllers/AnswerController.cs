using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        private readonly ITestResultService _testResultService;

        public AnswerController(IPermissionService permissionService, IChapterService chapterService, IQuestionService questionService, IAnswerService answerService, ITestResultService testResultService) {
            _permissionService = permissionService;
            _chapterService = chapterService;
            _questionService = questionService;
            _answerService = answerService;
            _testResultService = testResultService;
        }

        // GET: Answer
        public ActionResult DoTest(int chapterId) {
            if (!_permissionService.Authorize("Answer.DoTest"))
                return RedirectToRoute("Login");

            var chapter = _chapterService.GetChapterById(chapterId);

            //TODO: need to move this in factory class
            var model = chapter.ToModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult DoTest(ChapterModel chapterModel)
        {
            //if (!_permissionService.Authorize("Answer.DoTest"))
            //    return RedirectToRoute("Login");

            var chapter = _chapterService.GetChapterById(chapterModel.Id);

            ////TODO: need to move this in factory class
            var model = chapter.ToModel();

            return View(model);
        }
    }
}