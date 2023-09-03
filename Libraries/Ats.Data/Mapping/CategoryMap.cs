using TWYK.Core.Domain;

namespace TWYK.Data.Mapping
{
    public class CategoryMap : AtsEntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            this.ToTable("Category");
            this.HasKey(c => c.Id);
            this.Property(c => c.Name).IsRequired();

        }
    }
}