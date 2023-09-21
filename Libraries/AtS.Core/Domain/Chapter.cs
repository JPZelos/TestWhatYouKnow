using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TWYK.Core.Domain
{
    [Table("Chapter")]
    public class Chapter : BaseEntity

    {
        private ICollection<Question> _questions;

        [Column("TopicId")]
        [Display(Name = "Chapter TopicId")]
        public int TopicId { get; set; }

        [Column("Name")]
        [Display(Name = "Τίτλος")]
        [Required(ErrorMessage = "Ο Τίτλος είναι απαραίτητος")]
        public string Name { get; set; }
        
        [Column("Description")]
        [Display(Name = "Chapter Description")]
        [AllowHtml]
        public string Description { get; set; }

        [Column("SuccessMsg")]
        [Display(Name = "Μήνυμα απόλυτης επιτυχίας")]
        [Required(ErrorMessage = "Το Μήνυμα είναι απαραίτητο")]
        public string SuccessMsg { get; set; }

        [Column("PassMsg")]
        [Display(Name = "Μήνυμα επιτυχίας")]
        [Required(ErrorMessage = "Το Μήνυμα είναι απαραίτητο")]
        public string PassMsg { get; set; }

        [Column("FaultMsg")]
        [Display(Name = "Μήνυμα αποτυχίας")]
        [Required(ErrorMessage = "Το Μήνυμα είναι απαραίτητο")]
        public string FaultMsg { get; set; }


        [Column("PasScore")]
        [Display(Name = "Βάση Επιτυχίας")]
        [Required(ErrorMessage = "Η Βάση Επιτυχίας είναι απαραίτητη")]
        public int PasScore { get; set; } = 50;


        public virtual Topic Topic { get; set; }

        public virtual ICollection<Question> Questions {
            get => _questions ?? (_questions = new List<Question>());
            set => _questions = value;
        }

    }


}