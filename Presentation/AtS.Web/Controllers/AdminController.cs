using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web.Mvc;
using TWYK.Core;
using TWYK.Core.Domain;
using TWYK.Services.Chapters;
using TWYK.Services.Customers;
using TWYK.Services.Questions;
using TWYK.Services.Quizzes;
using TWYK.Services.Security;
using TWYK.Services.Topics;
using TWYK.Web.Framework.Controllers;
using TWYK.Web.Infrastructure.Mapper;
using TWYK.Web.Models;

namespace TWYK.Web.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionService;
        private readonly ICustomerService _customerService;
        private readonly ITopicService _topicService;
        private readonly IChapterService _chapterService;
        private readonly IQuestionService _questionService;
        private readonly IQuizService _quizService;

        public AdminController(
            IWorkContext workContext,
            IPermissionService permissionService,
            ICustomerService customerService,
            ITopicService topicService,
            IChapterService chapterService,
            IQuestionService questionService,
            IQuizService quizService
        ) {
            _workContext = workContext;
            _permissionService = permissionService;
            _customerService = customerService;
            _topicService = topicService;
            _chapterService = chapterService;
            _questionService = questionService;
            _quizService = quizService;
        }

        // GET: Admin

        #region Common

        public ActionResult Index() {
            return View();
        }

        [ChildActionOnly]
        public virtual ActionResult AdminNavLinks() {
            var model = PrepareAdminNavLinks();
            return PartialView(model);
        }

        private AdminNavLinksModel PrepareAdminNavLinks() {
            var customer = _workContext.CurrentCustomer;
            var alertMessage = string.Empty;

            var model = new AdminNavLinksModel {
                IsAuthenticated = customer.IsRegistered(),
                Customer = customer,
                CustomerName = customer.FirstName + " " + customer.LastName,
            };

            return model;
        }

        public ActionResult ActionDenied() {
            return View();
        }

        #endregion

        public ActionResult AdminUsers() {
            if (!_permissionService.Authorize("Admin.AdminUsers")) {
                return RedirectToRoute("ActionDenied");
            }

            var model = _customerService.GetAll().ToList();
            return View(model);
        }

        public ActionResult AdminTeacherUsers() {
            if (!_permissionService.Authorize("Admin.TeacherUsers")) {
                return RedirectToRoute("ActionDenied");
            }

            var customer = _workContext.CurrentCustomer;

            var model = new TeacherUsersModel();
            model.Teacher = customer;

            var students = _customerService.GetAllByTeacher(customer.Id);
            model.Students = students.ToList();

            return View(model);
        }

        public ActionResult GetUserResults(int stundentId, int teacherId) {
            if (!_permissionService.Authorize("Admin.TeacherUsers")) {
                return RedirectToRoute("ActionDenied");
            }

            var model = new ResultModel();

            var student = _customerService.GetCustomerById(stundentId);
            var userQuizzes = _quizService.GetAllUserQuizs(stundentId);
            var topicIds = userQuizzes.Select(q => q.Chapter.TopicId).Distinct().ToList();

            model.FullName = student.FirstName + " " + student.LastName;

            var modelTopics = new List<TopicModel>();

            foreach (var topicId in topicIds) {
                var topic = _topicService.GetTopicById(topicId).ToModel();
                var chapterIds = userQuizzes.Select(q => q.ChapterId).Distinct().ToList();

                if (topic.CustomerId == teacherId) {
                    foreach (var chapterId in chapterIds) {
                        var chapterModel = _chapterService.GetChapterById(chapterId).ToModel();
                        chapterModel.Quizzes = userQuizzes.Where(q => q.ChapterId == chapterId).ToList();
                        topic.Chapters.Add(chapterModel);
                    }
                }

                modelTopics.Add(topic);
            }

            model.Topics = modelTopics;

            return View(model);
        }

        #region Topics

        public ActionResult GetTecherTopics() {
            if (!_permissionService.Authorize("Admin.TeacherTopics")) {
                return RedirectToRoute("ActionDenied");
            }

            var teacher = _workContext.CurrentCustomer;
            var topics = _topicService.GetTopicByUserId(teacher.Id);

            return View(topics);
        }

        public ActionResult GetTecherTopic(int topicId) {
            if (!_permissionService.Authorize("Admin.TeacherTopics")) {
                return RedirectToRoute("ActionDenied");
            }

            Topic topic;

            var teacher = _workContext.CurrentCustomer;
            
            if (topicId == 0) {
                topic = new Topic();
            }
            else {
                topic = _topicService.GetTopicById(topicId);
            }

            return View(topic);
        }

        [HttpPost]
        public ActionResult GetTecherTopic(Topic model) {
            if (!_permissionService.Authorize("Admin.TeacherTopics")) {
                return RedirectToRoute("ActionDenied");
            }
            var teacher = _workContext.CurrentCustomer;

            Topic topic;
            if (model.Id != 0) {
                topic = _topicService.GetTopicById(model.Id);
            }
            else {
                topic = model;
            }

            if (ModelState.IsValid) {
                topic.Name = model.Name;
                topic.Description = model.Description;
                if (model.Id != 0) {
                    _topicService.UpdateTopic(topic);
                }
                else {
                    topic.CustomerId = teacher.Id;
                    _topicService.InsertTopic(topic);
                }
                
            }

            return View(topic);
        }

        #endregion

        #region Chpaters

        public ActionResult TeacherChapters() {
            if (!_permissionService.Authorize("Admin.TeacherTopics")) {
                return RedirectToRoute("ActionDenied");
            }

            var teacher = _workContext.CurrentCustomer;
            var chapters = _chapterService.GetChaptersByUserId(teacher.Id);

            return View(chapters);
        }

        public ActionResult TeacherChapter(int chapterId, int topicId=0)
        {
            if (!_permissionService.Authorize("Admin.TeacherTopics"))
            {
                return RedirectToRoute("ActionDenied");
            }

            Chapter chapter;
            if(chapterId != 0)
                chapter = _chapterService.GetChapterById(chapterId);
            else {
                chapter = new Chapter {
                    TopicId = topicId
                };
            }

            return View(chapter);
        }

        [HttpPost]
        public ActionResult TeacherChapter(Chapter model) {
            if (!_permissionService.Authorize("Admin.TeacherTopics")) {
                return RedirectToRoute("ActionDenied");
            }

            Chapter chapter;
            if (model.Id != 0)
                chapter = _chapterService.GetChapterById(model.Id);
            else {
                chapter = new Chapter {
                    TopicId = model.TopicId
                };
            }

            if (ModelState.IsValid) {
                chapter.Name = model.Name;
                chapter.Description = model.Description;
                chapter.PasScore = model.PasScore;
                if (model.Id != 0)
                    _chapterService.UpdateChapter(chapter);
                else {
                    _chapterService.InsertChapter(chapter);
                }
            }

            return View(chapter);
        }

        #endregion
        
        #region Questions

        public ActionResult Questions(int chapterId)
        {
            if (!_permissionService.Authorize("Admin.TeacherTopics"))
            {
                return RedirectToRoute("ActionDenied");
            }
            

            var teacher = _workContext.CurrentCustomer;
            var questions = _questionService.GetQuestionsByChapterId(chapterId);

            return View(questions);
        }

        public ActionResult EditQuestion(int questionId, int chapterId = 0)
        {
            if (!_permissionService.Authorize("Admin.TeacherTopics"))
            {
                return RedirectToRoute("ActionDenied");
            }

            Question question;
            if (questionId != 0) {
                question = _questionService.GetQuestionById(questionId);
            }
            else {
                question = new Question {
                    ChapterId = chapterId
                };
            }

            return View(question);
        }

        [HttpPost]
        public ActionResult EditQuestion(Question model)
        {
            if (!_permissionService.Authorize("Admin.TeacherTopics"))
            {
                return RedirectToRoute("ActionDenied");
            }

            Question question;
            
            if (model.Id != 0) 
                question = _questionService.GetQuestionById(model.Id);
            else {
                question = new Question {
                    ChapterId = model.ChapterId,
                    Description = model.Description,
                    Score = model.Score,
                    SuccessValue = model.SuccessValue,
                    FaultMsg = model.FaultMsg,
                    SuccessMsg = model.SuccessMsg,
                };
                _questionService.InsertQuestion(question);
            }

            if (ModelState.IsValid)
            {
                question.Score = model.Score;
                question.Description = model.Description;
                question.SuccessValue = model.SuccessValue;

                question.SuccessMsg = model.SuccessMsg;
                question.FaultMsg = model.FaultMsg;
                

                _questionService.UpdateQuestion(question);
            }

            return View(question);
        }

        #endregion
    }
}