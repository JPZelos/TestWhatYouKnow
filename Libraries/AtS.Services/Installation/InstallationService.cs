using System.Collections.Generic;
using System.Linq;
using TWYK.Core.Data;
using TWYK.Core.Domain;
using TWYK.Services.Seo;

namespace TWYK.Services.Installation
{
    public interface IInstallationService
    {
        void InstallSampleData();
        bool CanConnectToDb();
    }

    public class InstallationService : IInstallationService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;

        public InstallationService(IRepository<Product> productRepository, IRepository<Category> categoryRepository) {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public void InstallSampleData() {
            var categories = _categoryRepository.Table.ToList();
            var products = _productRepository.Table.ToList();

            // Init Categories
            if (categories.Count == 0) {
                categories = InitCategories();
            }

            if (products.Count == 0) {
                products = FakeProductFactory.GetSomeProducts(categories, 4, 8);

                _productRepository.Insert(products);

                foreach (var cat in categories) {
                    var catPrds = cat.Products.ToList();
                    for (var i = 0; i < catPrds.Count; i++) {
                        var prd = catPrds[i];
                        var pictureName = SeoExtensions.GetSeName($"{prd.Category.Name}-{i + 1}", true) + ".jpg";
                        prd.Picture = pictureName;
                    }

                    _productRepository.Update(catPrds);
                }
            }
        }

        public bool CanConnectToDb() {
            return _productRepository.CanConnectToDb(new Product());
        }

        private List<Category> InitCategories() {
            var categories = new List<Category> {
                new Category {
                    Name = "Soccer",
                    Description =
                        "Soccer  boots, called cleats or soccer shoes in North America,[1] are an item of footwear worn when playing association football. Those designed for grass pitches have studs on the outsole to aid grip."
                },
                new Category {
                    Name = "Running",
                    Description =
                        "Running shoes are footwear designed specifically to help you run in a way that will prevent injury and increase your athletic performance as a runner. They come in all sorts of styles and sizes—from minimalist shoes without many extra features to tricked-out types that help you as a runner."
                },
                new Category {
                    Name = "Tennis",
                    Description =
                        "Tennis shoes are athletic shoes made specifically for the sport of tennis. Tennis shoes provide stability for quick sprints and rapid turns. Tennis shoes are slightly heavy with thick soles that provide a quick bounce when needed. These shoes are also good for a number of other sports, which is one of the reasons many people refer to all generic athletic shoes as “tennis shoes.” It should be noted. However, that true tennis shoes are beneficial when playing a match."
                },
                new Category {
                    Name = "Trekking",
                    Description =
                        "Hiking (walking) boots are footwear specifically designed for protecting the feet and ankles during outdoor walking activities such as hiking. They are one of the most important items of hiking gear, since their quality and durability can determine a hiker's ability to walk long distances without injury. Hiking boots are constructed to provide comfort for walking considerable distance over rough terrain."
                },
                new Category {
                    Name = "Bowling ",
                    Description =
                        "Bowling shoes are specifically made for use at bowling alleys. They are usually made with a leather upper and a rubber sole that is very slick on the bottom. Casual bowlers rent shoes at a bowling alley while those bowlers who are more serious purchase their own. Those who buy their own bowling shoes will find an array of colors and styles. They will also find that their shoes have a rubber stopper for the non-sliding foot. Rental shoes are painted in garish colors to discourage theft and rarely offer a rubber stopper."
                },
            };
            _categoryRepository.Insert(categories);
            return categories;
        }
    }
}