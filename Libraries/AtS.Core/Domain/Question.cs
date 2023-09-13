using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWYK.Core.Domain
{
    [Table("Question")]
    // Remove this line if you no have an entity validator
    //[Validator(typeof(QuestionValidator))]
    public class Question : BaseEntity
    {
        private ICollection<Answer> _answer;

        [Column("Id")]
        [Display(Name = "Question Id")]
        public int Id { get; set; }


        [Column("Description")]
        [Display(Name = "Ερώτηση")]
        [Required(ErrorMessage = "Η Ερώτηση είναι απαραίτητη")]
        public string Description { get; set; }


        [Column("Score")]
        [Display(Name = "Score")]
        public int Score { get; set; }
        

        [Column("ChapterId")]
        [Display(Name = "Question ChapterId")]
        public int ChapterId { get; set; }


        [Column("SuccessValue")]
        [Display(Name = "Επιτυχημένη Απάντηση")]
        [Required(ErrorMessage = "Η Επιτυχημένη Απάντηση είναι απαραίτητη")]
        public int SuccessValue { get; set; }

        public virtual Chapter Chapter { get; set; }

        public virtual ICollection<Answer> Answers {
            get => _answer ?? (_answer = new List<Answer>());
            set => _answer = value;
        }
    }
}