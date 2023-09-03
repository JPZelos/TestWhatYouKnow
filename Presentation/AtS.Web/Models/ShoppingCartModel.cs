using System.Collections.Generic;

namespace TWYK.Web.Models
{
    public class ShoppingCartModel
    {
        public ShoppingCartModel() {
            Items = new List<ShoppingCartItemModel>();
            Warnings = new List<string>();
        }

        public IList<ShoppingCartItemModel> Items { get; set; }

        public IList<string> Warnings { get; set; }

        public int TotalProducts { get; set; }

        public decimal SubTotal { get; set; }

        public virtual CustomerModel Customer { get; set; }
    }
}