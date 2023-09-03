using System.Linq;
using System.Web.Mvc;
using TWYK.Services.Categories;
using TWYK.Services.Installation;
using TWYK.Services.Products;
using TWYK.Web.Infrastructure.Mapper;

namespace TWYK.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInstallationService _installationService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public HomeController(
            IInstallationService installationService,
            ICategoryService categoryService,
            IProductService productService
        ) {
            _installationService = installationService;
            _categoryService = categoryService;
            _productService = productService;
        }

        public ActionResult Index() {
            var isDatabaseInstalled = _installationService.CanConnectToDb();
            if (!isDatabaseInstalled) {
                return RedirectToAction("DatabaseInfo", "Common");
            }

            //install some sample data in case of fresh install
            _installationService.InstallSampleData();

            //TODO: need to move this in factory class
            var model = _categoryService.GetAllCategories().ToModelList();
            foreach (var category in model) {
                category.Products = _productService.GetProductsByCategory(category.Id).Take(4).ToList().ToModelList();
            }

            return View(model.ToList());
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