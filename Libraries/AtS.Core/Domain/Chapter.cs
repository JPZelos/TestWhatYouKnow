using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TWYK.Core.Domain
{
    [Table("Chapter")]
    // Remove this line if you no have an entity validator
    //[Validator(typeof(ChapterValidator))]
    public class Chapter : BaseEntity

    {
        private ICollection<Question> _question;

        [Column("Id")]
        [Display(Name = "Chapter Id")]
        public int Id { get; set; }


        [Column("TopicId")]
        [Display(Name = "Chapter TopicId")]
        public int TopicId { get; set; }


        [Column("Name")]
        [Display(Name = "Chapter Name")]
        public string Name { get; set; }


        [Column("Description")]
        [Display(Name = "Chapter Description")]
        public string Description { get; set; }


        public virtual Topic Topic { get; set; }

        public virtual ICollection<Question> Questions {
            get => _question ?? (_question = new List<Question>());
            set => _question = value;
        }
    }


}