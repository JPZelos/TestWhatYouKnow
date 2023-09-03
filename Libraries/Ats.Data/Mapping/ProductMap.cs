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

    public class QuestionMap : AtsEntityTypeConfiguration<Question>
    {
        public QuestionMap()
        {
            this.ToTable("Question");
            this.HasKey(c => c.Id);
            this.Property(c => c.Description).IsRequired();

            this.HasRequired(q => q.Chapter)
                .WithMany(c => c.Questions)
                .HasForeignKey(q => q.ChapterId);
        }
    }

    public class AnswerMap : AtsEntityTypeConfiguration<Answer>
    {
        public AnswerMap()
        {
            this.ToTable("Answer");
            this.HasKey(c => c.Id);
            this.Property(c => c.Label).IsRequired();

            this.HasRequired(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);
        }
    }

    public class TestResultMap : AtsEntityTypeConfiguration<TestResult>
    {
        public TestResultMap()
        {
            this.ToTable("TestResult");
            this.HasKey(c => c.Id);

            this.Property(c => c.AnswerId).IsRequired();

        }
    }
}