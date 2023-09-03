using TWYK.Core.Domain;

namespace TWYK.Data.Mapping
{
    public class ShoppingCartItemMap : AtsEntityTypeConfiguration<ShoppingCartItem>
    {
        public ShoppingCartItemMap()
        {
            this.ToTable("ShoppingCartItem");
            this.HasKey(c => c.Id);
            //this.Property(c => c.CustomerId).IsRequired();
            //this.Property(c => c.ProductId).IsRequired();

            //HasRequired(td => td.Product);
            //HasRequired(td => td.Customer);

            this.HasRequired(sci => sci.Customer)
                .WithMany(c => c.ShoppingCartItems)
                .HasForeignKey(sci => sci.CustomerId);

            this.HasRequired(sci => sci.Product)
                .WithMany()
                .HasForeignKey(sci => sci.ProductId);
        }
    }
}