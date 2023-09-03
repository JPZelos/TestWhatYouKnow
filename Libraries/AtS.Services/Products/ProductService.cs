using System.Collections.Generic;
using System.Linq;
using TWYK.Core.Data;
using TWYK.Core.Domain;

namespace TWYK.Services.Products
{
    public interface IProductService
    {
        /// <summary>
        /// Gets product
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        Product GetProductById(int productId);

        decimal GetProductPrice(int productId);
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetAllProducts();
    }

    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository) {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Gets product
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        public virtual Product GetProductById(int productId)
        {
            if (productId == 0)
                return null;

            return _productRepository.GetById(productId);
        }

        public decimal GetProductPrice(int productId) {
            if (productId == 0)
                return 0;
            var product = _productRepository.GetById(productId);
            var totalPrice = (product.Price - ((product.DiscountPerCent * product.Price) / 100)).Round();
            return totalPrice;
        }

        public virtual List<Product> GetAllProducts() {

            return _productRepository.Table.ToList();
        }

        public virtual List<Product> GetProductsByCategory(int categoryId) {
            if (categoryId == 0)
                return null;

            return _productRepository.Table.Where(p=>p.CategoryId==categoryId).ToList();
        }
    }
}