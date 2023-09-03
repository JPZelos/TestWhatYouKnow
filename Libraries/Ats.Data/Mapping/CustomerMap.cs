using TWYK.Core.Domain;

namespace TWYK.Data.Mapping
{
    public partial class CustomerMap : AtsEntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.ToTable("Customer");
            this.HasKey(c => c.Id);
            this.Property(u => u.UserName).HasMaxLength(1000);
            this.Property(u => u.Email).HasMaxLength(1000);
        }
    }
}