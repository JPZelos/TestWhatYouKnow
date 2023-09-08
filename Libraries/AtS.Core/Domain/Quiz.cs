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
        [Display(Name = "Quiz_ChapterId")]
        public int ChapterId { get; set; }


        [Column("Score")]
        [Display(Name = "Quiz_Score")]
        public int Score { get; set; }


        [Column("Tries")]
        [Display(Name = "Quiz_Tries")]
        public int Tries { get; set; }


        [Column("Success")]
        [Display(Name = "Quiz_Success")]
        public bool Success { get; set; }

        public virtual Chapter Chapter { get; set; }
        
        public virtual Customer Customer { get; set; }
    }
}
