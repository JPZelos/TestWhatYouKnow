using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TWYK.Core;
using TWYK.Core.Domain;

namespace TWYK.Web.Models
{
    public class QuestionModel : BaseEntity
    {

        //[Display(Name = "Question Id")]
        //public int Id { get; set; }


        [Display(Name = "Question Description")]
        public string Description { get; set; }


        [Display(Name = "Question Score")]
        public int Score { get; set; }
        
        [Display(Name = "Question Success Msg")]
        public string SuccessMsg { get; set; }


        [Display(Name = "Question Fault Msg")]
        public string FaultMsg { get; set; }


        [Display(Name = "Question ChapterId")]
        public int ChapterId { get; set; }


        [Display(Name = "Question Success Value")]
        public int SuccessValue { get; set; }

        public bool IsSuccess { get; set; }

        public virtual Chapter Chapter { get; set; }
        
        public IList<Answer> Answers { get; set; }
    }
}