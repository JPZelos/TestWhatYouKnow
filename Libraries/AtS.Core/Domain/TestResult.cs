using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TWYK.Core.Domain
{
    [Table("TestResult")]
    public class TestResult : BaseEntity
    {

        [Column("CustomerId")]
        [Display(Name = "Test Result_CustomerId")]
        public int CustomerId { get; set; }


        [Column("AnswerId")]
        [Display(Name = "Test Result AnswerId")]
        public int AnswerId { get; set; }


        [Column("QuizId")]
        [Display(Name = "Test Result QuizId")]
        public int QuizId { get; set; }


        [Column("Score")]
        [Display(Name = "TestResult Score")]
        public int Score { get; set; }

        [Column("Success")]
        [Display(Name = "TestResult Success")]
        public bool Success { get; set; }

        public virtual Answer Answer { get; set; }
    }


}