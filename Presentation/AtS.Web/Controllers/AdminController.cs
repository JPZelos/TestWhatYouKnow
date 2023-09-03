using System.Web.Mvc;
using TWYK.Services.Categories;
using TWYK.Services.Products;
using TWYK.Web.Framework.Controllers;
using TWYK.Web.Infrastructure.Mapper;

namespace TWYK.Web.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public AdminController(
            IProductService productService,
            ICategoryService categoryService
        ) {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: Admin
        public ActionResult Index() {
            return View();
        }

        public ActionResult Categories() {
            var model = _categoryService.GetAllCategories().ToModelList();
            return View(model);
        }

        public ActionResult Products() {
            var model = _productService.GetAllProducts().ToModelList();
            return View(model);
        }
    }
}