using TWYK.Core.Domain;

namespace TWYK.Web.Models
{
    public class NavLinksModel
    {
        public Customer Customer { get; set; }

        public bool IsAuthenticated { get; set; }

        public string CustomerName { get; set; }

        public bool ShoppingCartEnabled { get; set; }

        public int ShoppingCartItems { get; set; }

        public bool WishlistEnabled { get; set; }

        public int WishlistItems { get; set; }


        public string AlertMessage { get; set; }
    }


    public class AdminNavLinksModel
    {
        public Customer Customer { get; set; }

        public bool IsAuthenticated { get; set; }

        public string CustomerName { get; set; }
    }
}