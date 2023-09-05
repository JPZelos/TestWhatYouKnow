using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TWYK.Web.Models
{
    public class TestResults
    {
        public TestResults() {
            Questions = new List<QuestionModel>();
            Results = new List<TestResultModel>();
        }

       public List<QuestionModel> Questions { get; set; }
       public List<TestResultModel> Results { get; set; }
    }
}