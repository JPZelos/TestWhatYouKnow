using System.Collections.Generic;
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

        public ActionResult GetTecherTopics() {
            if (!_permissionService.Authorize("Admin.TeacherTopics")) {
                return RedirectToRoute("ActionDenied");
            }
            var teacher = _workContext.CurrentCustomer;
            var topics = _topicService.GetTopicByUserId(teacher.Id);


            return View(topics);
        }

        public ActionResult GetTecherTopic(int topicId)
        {
            if (!_permissionService.Authorize("Admin.TeacherTopics"))
            {
                return RedirectToRoute("ActionDenied");
            }
            var teacher = _workContext.CurrentCustomer;
            var topic = _topicService.GetTopicById(topicId);


            return View(topic);
        }
    }
}