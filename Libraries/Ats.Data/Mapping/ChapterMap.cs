using TWYK.Core.Domain;

namespace TWYK.Data.Mapping
{
    public class ChapterMap : AtsEntityTypeConfiguration<Chapter>
    {
        public ChapterMap() {
            this.ToTable("Chapter");
            this.HasKey(c => c.Id);
            this.Property(c => c.Name).IsRequired();

            this.HasRequired(c=>c.Topic)
                .WithMany(t =>t.Chapters)
                .HasForeignKey(c => c.TopicId);
        }
    }
}