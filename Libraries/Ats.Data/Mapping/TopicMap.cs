using TWYK.Core.Domain;

namespace TWYK.Data.Mapping
{
    public class TopicMap : AtsEntityTypeConfiguration<Topic>
    {
        public TopicMap()
        {
            this.ToTable("Topic");
            this.HasKey(c => c.Id);
            this.Property(c => c.Name).IsRequired();

            // Define One to Many Relationship
            this.HasRequired(topic => topic.Customer)
                .WithMany(c => c.Topics)
                .HasForeignKey(c => c.CustomerId);
        }
    }
}