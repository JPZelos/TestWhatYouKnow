using System.Web.Mvc;
using TWYK.Services.Answers;
using TWYK.Services.Chapters;
using TWYK.Services.Questions;
using TWYK.Services.Security;
using TWYK.Services.TestResults;
using TWYK.Services.Topics;
using TWYK.Web.Infrastructure.Mapper;

namespace TWYK.Web.Controllers
{
    //[Authorize]
    public class CatalogController : BaseController
    {

        private readonly IPermissionService _permissionService;
        private readonly IChapterService _chapterService;

        public CatalogController(
            IPermissionService permissionService,
            IChapterService chapterService
        ) {
            _permissionService = permissionService;
            _chapterService = chapterService;
        }


        public ActionResult ChaptertDetails(int chapterId) {
            if (!_permissionService.Authorize("Catalog.ChaptertDetails"))
                return RedirectToRoute("Login");
                //return RedirectToAction("ChaptertSummary", new { chapterId = chapterId });

            var chapter = _chapterService.GetChapterById(chapterId);
            var model = chapter.ToModel();

            return View(model);
        }

        public ActionResult ChaptertSummary(int chapterId) {

            var chapter = _chapterService.GetChapterById(chapterId);
            var model = chapter.ToModel();

            return View(model);
        }

    }
}