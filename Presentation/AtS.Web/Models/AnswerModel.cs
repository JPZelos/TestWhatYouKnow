using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TWYK.Core;
using TWYK.Core.Domain;

namespace TWYK.Web.Models
{
    public class AnswerModel : BaseEntity
    {

        //[Display(Name = "Answer_Id")]
        //public int Id { get; set; }


        [Display(Name = "Answer QuestionId")]
        public int QuestionId { get; set; }


        [Display(Name = "Answer Label")]
        public string Label { get; set; }


        [Display(Name = "Answer Value")]
        public int Value { get; set; }

        public virtual QuestionModel Question { get; set; }
    }
}