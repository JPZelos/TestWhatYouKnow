using TWYK.Core.Domain;

namespace TWYK.Data.Mapping
{
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