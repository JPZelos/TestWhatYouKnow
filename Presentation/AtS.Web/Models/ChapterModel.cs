using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TWYK.Core;
using TWYK.Core.Domain;

namespace TWYK.Web.Models
{
    public class ChapterModel : BaseEntity

    {
       
        public ChapterModel() {
            Quizzes = new List<Quiz>();
        }

        [Display(Name = "Chapter TopicId")]
        public int TopicId { get; set; }

        public int QuizId { get; set; }

        [Display(Name = "Chapter Name")]
        public string Name { get; set; }


        [Display(Name = "Chapter Description")]
        public string Description { get; set; }

        public int PasScore { get; set; } = 50;

        public  TopicModel Topic { get; set; }

        public int SuccessProgres() {
            var count = Quizzes.Count(q => q.Success);
                return count;

        }
       
        public IList<QuestionModel> Questions { get; set; }

        public IList<Quiz> Quizzes { get; set; }

    }


}