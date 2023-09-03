using System.Collections.Generic;
using TWYK.Core;

namespace TWYK.Web.Models
{
    public class CategoryModel : BaseEntity
    {
        private ICollection<ProductModel> _products;

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }


        public bool Active { get; set; }


        public string ActiveClass => this.Active ? "active" : null;

        /// <summary>
        /// Gets or sets the Category Products
        /// </summary>
        public virtual ICollection<ProductModel> Products {
            get => _products ??= new List<ProductModel>();
            set => _products = value;
        }
    }
}