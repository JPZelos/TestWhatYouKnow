using System.Collections.Generic;
using TWYK.Core;
using TWYK.Core.Domain;

namespace TWYK.Web.Models
{
   public class ShoppingCartItemModel : BaseEntity
    {
        public ShoppingCartItemModel() {
            Warnings = new List<string>();
        }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the product
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets the customer
        /// </summary>
        public virtual Customer Customer { get; set; }

        public IList<string> Warnings { get; set; }
    }
}
