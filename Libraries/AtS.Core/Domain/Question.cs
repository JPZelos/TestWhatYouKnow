﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWYK.Core.Domain
{
    [Table("Question")]
    public class Question : BaseEntity
    {
        private ICollection<Answer> _answer;

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
        [Display(Name = "Σωστή απάντηση")]
        [Required(ErrorMessage = "Η Σωστή απάντηση είναι απαραίτητη")]
        public int SuccessValue { get; set; }

        public virtual Chapter Chapter { get; set; }

        public virtual ICollection<Answer> Answers {
            get => _answer ?? (_answer = new List<Answer>());
            set => _answer = value;
        }
    }
}