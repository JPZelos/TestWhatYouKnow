using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWYK.Core.Domain
{
    [Table("Answer")]
    // Remove this line if you no have an entity validator
    //[Validator(typeof(AnswerValidator))]
    public class Answer : BaseEntity
    {
        [Column("Id")]
        [Display(Name = "Answer_Id")]
        public int Id { get; set; }


        [Column("QuestionId")]
        [Display(Name = "Answer QuestionId")]
        public int QuestionId { get; set; }


        [Column("Label")]
        [Display(Name = "Answer Label")]
        public string Label { get; set; }


        [Column("Value")]
        [Display(Name = "Answer Value")]
        public int Value { get; set; }

        public virtual Question Question { get; set; }
    }
}