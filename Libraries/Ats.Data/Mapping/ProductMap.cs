using TWYK.Core.Domain;

namespace TWYK.Data.Mapping
{
    public class ProductMap : AtsEntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            this.ToTable("Product");
            this.HasKey(c => c.Id);
            this.Property(c => c.Name).IsRequired();
            this.Property(c => c.Price).IsRequired();

            // Define One to Many Relationship
            this.HasRequired(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId);

        }
    }
}