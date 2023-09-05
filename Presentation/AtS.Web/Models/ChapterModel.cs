using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TWYK.Core;
using TWYK.Core.Domain;

namespace TWYK.Web.Models
{
    public class ChapterModel : BaseEntity

    {
        //[Display(Name = "Chapter Id")]
        //public int Id { get; set; }

        [Display(Name = "Chapter TopicId")]
        public int TopicId { get; set; }


        [Display(Name = "Chapter Name")]
        public string Name { get; set; }


        [Display(Name = "Chapter Description")]
        public string Description { get; set; }
        
        public  TopicModel Topic { get; set; }

        public IList<QuestionModel> Questions { get; set; }
    }


}