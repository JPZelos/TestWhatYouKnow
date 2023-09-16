using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TWYK.Core.Domain
{
    [Table("Topic")]
    public class Topic : BaseEntity
    {
        private ICollection<Chapter> _chapter;
        
        [Column("CustomerId")]
        [Display(Name = "User Id")]
        public int CustomerId { get; set; }
        
        [Column("Name")]
        [Display(Name = "Τίτλος")]
        [Required(ErrorMessage = "Ο Τίτλος είναι απαραίτητος")]
        public string Name { get; set; }


        [Column("Description")]
        [Display(Name = "Περιγραφή")]
        [AllowHtml]
        public string Description { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<Chapter> Chapters {
            get => _chapter ?? (_chapter = new List<Chapter>());
            set => _chapter = value;
        }
    }


}