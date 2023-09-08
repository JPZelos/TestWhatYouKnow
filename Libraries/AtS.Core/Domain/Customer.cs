using System;
using System.Collections.Generic;

namespace TWYK.Core.Domain
{
    public class Customer : BaseEntity
    {
        private ICollection<ShoppingCartItem> _shoppingCartItems;
        private ICollection<Topic> _topics;
        private ICollection<Quiz> _quizzes;
        /// <summary>
        /// Ctor
        /// </summary>
        public Customer()
        {
            this.CustomerGuid = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the customer Guid
        /// </summary>
        public Guid CustomerGuid { get; set; }

        public string RoleNames { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this customer has some products in the shopping cart
        /// <remarks>The same as if we run this.ShoppingCartItems.Count > 0
        /// We use this property for performance optimization:
        /// if this property is set to false, then we do not need to load "ShoppingCartItems" navigation property for each page load
        /// It's used only in a couple of places in the presenation layer
        /// </remarks>
        /// </summary>
        public bool HasShoppingCartItems { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last login
        /// </summary>
        public DateTime? LastLoginDateUtc { get; set; }

        /// <summary>
        /// Gets or sets shopping cart items
        /// </summary>
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems {
            get => _shoppingCartItems ?? (_shoppingCartItems = new List<ShoppingCartItem>());
            set  => _shoppingCartItems = value;
        }

        public virtual ICollection<Quiz> Quizzes {
            get => _quizzes ?? (_quizzes = new List<Quiz>());
             set => _quizzes = value;
        }

        public virtual ICollection<Topic> Topics {
            get => _topics ?? (_topics = new List<Topic>());
            set => _topics = value;
        }
    }
}