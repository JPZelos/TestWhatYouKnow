using System.Linq;
using System.Web.Mvc;
using TWYK.Services.Answers;
using TWYK.Services.Categories;
using TWYK.Services.Chapters;
using TWYK.Services.Installation;
using TWYK.Services.Products;
using TWYK.Services.Questions;
using TWYK.Services.TestResults;
using TWYK.Services.Topics;
using TWYK.Web.Infrastructure.Mapper;

namespace TWYK.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInstallationService _installationService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        private readonly ITopicService _topicService;
        private readonly IChapterService _chapterService;
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly ITestResultService _testResultService;

        public HomeController(
            IInstallationService installationService,
            ICategoryService categoryService,
            IProductService productService,
            ITopicService topicService,
            IChapterService chapterService,
            IQuestionService questionService,
            IAnswerService answerService,
            ITestResultService testResultService
        ) {
            _installationService = installationService;
            _categoryService = categoryService;
            _productService = productService;
            _topicService = topicService;
            _chapterService = chapterService;
            _questionService = questionService;
            _answerService = answerService;
            _testResultService = testResultService;
        }

        public ActionResult Index() {
            var isDatabaseInstalled = _installationService.CanConnectToDb();
            if (!isDatabaseInstalled) {
                return RedirectToAction("DatabaseInfo", "Common");
            }

            //install some sample data in case of fresh install
            _installationService.InstallSampleData();

            //TODO: need to move this in factory class
            //var model = _categoryService.GetAllCategories().ToModelList();
            //foreach (var category in model) {
            //    category.Products = _productService.GetProductsByCategory(category.Id).Take(4).ToList().ToModelList();
            //}
            //return View(model.ToList());

            var model = _topicService.GetAllTopics().ToModelList();
            foreach (var topic in model) {
                topic.Chapters = _chapterService.GetAllChapters().ToModelList();
            }

            return View(model);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}