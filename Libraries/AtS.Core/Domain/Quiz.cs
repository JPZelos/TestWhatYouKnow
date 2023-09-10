using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWYK.Core.Domain
{
    public class Quiz : BaseEntity
    {
        [Column("CustomerId")]
        [Display(Name = "Quiz_CustomerId")]
        public int CustomerId { get; set; }


        [Column("ChapterId")]
        [Display(Name = "Quiz ChapterId")]
        public int ChapterId { get; set; }


        [Column("Score")]
        [Display(Name = "Quiz Score")]
        public int Score { get; set; }


        [Column("Tries")]
        [Display(Name = "Quiz Tries")]
        public int Tries { get; set; }


        [Column("Success")]
        [Display(Name = "Quiz Success")]
        public bool Success { get; set; }


        [Column("LastUpdated")]
        [Display(Name = "Quiz Last Updated")]
        public DateTime LastUpdated { get; set; }

        public virtual Chapter Chapter { get; set; }
        
        public virtual Customer Customer { get; set; }
    }
}
