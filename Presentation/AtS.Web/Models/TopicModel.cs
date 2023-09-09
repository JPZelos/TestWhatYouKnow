using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TWYK.Core;
using TWYK.Core.Domain;

namespace TWYK.Web.Models
{
    public class TopicModel : BaseEntity {

        //[Display(Name = "Topic Id")]
        //public int Id { get; set; }


        [Display(Name = "User Id")]
        public int CustomerId { get; set; }
        
        [Display(Name = "Topic Name")]
        public string Name { get; set; }


        [Display(Name = "Topic Description")]
        public string Description { get; set; }

        public virtual CustomerModel Customer { get; set; }

        public IList<ChapterModel> Chapters { get; set; }

       
    }


}