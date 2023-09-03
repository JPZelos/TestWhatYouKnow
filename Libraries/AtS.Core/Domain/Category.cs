using System.Collections.Generic;

namespace TWYK.Core.Domain
{
    public class Category : BaseEntity
    {
        private ICollection<Product> _products;

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Category Products
        /// </summary>
        public virtual ICollection<Product> Products {
            get => _products ?? (_products = new List<Product>());
            set => _products = value;
        }
    }
}