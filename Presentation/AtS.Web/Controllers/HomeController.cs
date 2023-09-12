using System.Web.Mvc;
using TWYK.Services.Chapters;
using TWYK.Services.Installation;
using TWYK.Services.Topics;
using TWYK.Web.Infrastructure.Mapper;

namespace TWYK.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInstallationService _installationService;

        private readonly ITopicService _topicService;
        private readonly IChapterService _chapterService;

        public HomeController(
            IInstallationService installationService,
            ITopicService topicService,
            IChapterService chapterService
        ) {
            _installationService = installationService;
            _topicService = topicService;
            _chapterService = chapterService;
        }

        public ActionResult Index() {
            var isDatabaseInstalled = _installationService.CanConnectToDb();
            if (!isDatabaseInstalled) {
                return RedirectToAction("DatabaseInfo", "Common");
            }

            _installationService.InstallSampleData();

            var model = _topicService.GetAllTopics().ToModelList();
            foreach (var topic in model) {
                topic.Chapters = _chapterService.GetChaptersByTopic(topic.Id).ToModelList();
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