using TWYK.Core.Domain;

namespace TWYK.Data.Mapping
{
    public class QuizMap : AtsEntityTypeConfiguration<Quiz>
    {
        public QuizMap()
        {
            this.ToTable("Quiz");
            this.HasKey(c => c.Id);

            //this.Property(c=>c.ChapterId).IsRequired();
            //this.Property(c=>c.CustomerId).IsRequired();

            //this.HasRequired(q => q.Customer).WithMany().HasForeignKey(q => q.CustomerId).WillCascadeOnDelete(false); ;
            //this.HasRequired(q => q.Chapter).WithMany().HasForeignKey(q => q.ChapterId);

            this.HasRequired(sci => sci.Customer)
                .WithMany(c => c.Quizzes)
                .HasForeignKey(sci => sci.CustomerId).WillCascadeOnDelete(false);

            this.HasRequired(sci => sci.Chapter)
                .WithMany()
                .HasForeignKey(sci => sci.ChapterId).WillCascadeOnDelete(false);
        }
    }
}