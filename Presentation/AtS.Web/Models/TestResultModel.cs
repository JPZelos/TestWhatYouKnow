using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TWYK.Core;
using TWYK.Core.Domain;

namespace TWYK.Web.Models
{
    public class TestResultModel : BaseEntity
    {
        
        [Display(Name = "Test Result Id")]
        public int Id { get; set; }


        [Display(Name = "Test Result_CustomerId")]
        public int CustomerId { get; set; }


        [Display(Name = "Test Result AnswerId")]
        public int AnswerId { get; set; }


        [Display(Name = "TestResult Score")]
        public int Score { get; set; }

        public virtual AnswerModel Answer { get; set; }
    }


}