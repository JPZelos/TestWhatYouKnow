using System.Linq;
using System.Web.Mvc;
using TWYK.Services.Categories;
using TWYK.Services.Products;
using TWYK.Services.Security;
using TWYK.Web.Infrastructure.Mapper;

namespace TWYK.Web.Controllers
{
    //[Authorize]
    public class CatalogController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IPermissionService _permissionService;

        public CatalogController(
            IProductService productService,
            ICategoryService categoryService,
            IPermissionService permissionService
        ) {
            _productService = productService;
            _categoryService = categoryService;
            _permissionService = permissionService;
        }

        // GET: Catalog
        public ActionResult Index() {
            if (!_permissionService.Authorize("Catalog.List"))
                return RedirectToRoute("PageNotFound");
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!_permissionService.Authorize("Catalog.List"))
                return RedirectToRoute("PageNotFound");
            
            //TODO: need to move this in factory class
            var model = _categoryService.GetAllCategories().ToModelList();
            foreach (var category in model) {
                category.Products = _productService.GetProductsByCategory(category.Id).ToModelList();
            }

            return View(model.ToList());
        }


        public ActionResult Category(int categoryId) {
            if (!_permissionService.Authorize("Catalog.List"))
                return RedirectToRoute("PageNotFound");


            var category = _categoryService.GetCategoryById(categoryId);
            var model = category.ToModel();

            return View(model);
        }


        public ActionResult ProductDetails(int productId) {
            var product = _productService.GetProductById(productId);

            //TODO: need to move this in factory class
            var model = product.ToModel();
            model.RelatedProducts = _productService.GetProductsByCategory(model.CategoryId).Where(p=>p.Id != productId).Take(4).ToList().ToModelList();

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult TopMenu() {
            var categories = _categoryService.GetAllCategories();
            var model = categories.ToModelList();

            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult SideBarMenu(int categoryId = 0)
        {
            var categories = _categoryService.GetAllCategories();
            var model = categories.ToModelList();

            foreach (var item in model) {
                if (item.Id == categoryId) {
                    item.Active = true;
                    break;
                }
            }

            return PartialView(model);
        }
    }
}