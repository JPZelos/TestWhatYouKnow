using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TWYK.Core.Domain
{
    [Table("Topic")]
    // Remove this line if you no have an entity validator
    //[Validator(typeof(TopicValidator))]
    public class Topic : BaseEntity
    {
        private ICollection<Chapter> _chapter;

        [Column("Id")]
        [Display(Name = "Topic Id")]
        public int Id { get; set; }


        [Column("CustomerId")]
        [Display(Name = "User Id")]
        public int CustomerId { get; set; }
        
        [Column("Name")]
        [Display(Name = "Τίτλος")]
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