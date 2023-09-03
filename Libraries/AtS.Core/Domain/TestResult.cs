﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TWYK.Core.Domain
{
    [Table("TestResult")]
    // Remove this line if you no have an entity validator
    //[Validator(typeof(TestResaultValidator))]
    public class TestResult : BaseEntity
    {
        [Column("Id")]
        [Display(Name = "Test Result Id")]
        public int Id { get; set; }


        [Column("CustomerId")]
        [Display(Name = "Test Result_CustomerId")]
        public int CustomerId { get; set; }


        [Column("AnswerId")]
        [Display(Name = "Test Result AnswerId")]
        public int AnswerId { get; set; }


        [Column("Score")]
        [Display(Name = "TestResult Score")]
        public int Score { get; set; }

        public virtual Answer Answer { get; set; }
    }


}