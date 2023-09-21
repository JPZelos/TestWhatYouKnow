using TWYK.Core.Domain;

namespace TWYK.Data.Mapping
{
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
}