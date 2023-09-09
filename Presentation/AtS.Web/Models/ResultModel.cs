using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TWYK.Core.Domain;

namespace TWYK.Web.Models
{
    public class ResultModel
    {
        public ResultModel() {
            Topics = new List<TopicModel>();
            Chapters = new List<ChapterModel>();
        }
        public string FullName { get; set; }

        public List<TopicModel> Topics { get; set; }

        public List<ChapterModel> Chapters { get; set; }

    }
}