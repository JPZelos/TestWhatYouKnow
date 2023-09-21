using TWYK.Core.Domain;

namespace TWYK.Data.Mapping
{
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
}