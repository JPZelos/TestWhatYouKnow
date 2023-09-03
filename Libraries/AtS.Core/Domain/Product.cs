namespace TWYK.Core.Domain
{
    /// <summary>
    /// Represents a product
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// Get or sets the category foreign key
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Brand name
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the short description
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the full description
        /// </summary>
        public string FullDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the product on home page
        /// </summary>
        public bool ShowOnHomePage { get; set; }

        /// <summary>
        /// Gets or sets the SKU
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets the price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the price Discount PerCent
        /// </summary>
        public int DiscountPerCent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        public string Picture { get; set; }

        public virtual Category Category { get; set; }

        
    }

    public static class ProductExtentions
    {
        public static decimal GetTotalPrice(this Product product) {
            return (product.Price - ((product.Price * product.DiscountPerCent) / 100)).Round();
        }
    }
}