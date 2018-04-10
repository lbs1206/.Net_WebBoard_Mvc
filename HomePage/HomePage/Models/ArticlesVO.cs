using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomePage.Models
{
    public class ArticlesVO
    {
        public int ArticleIDX { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public int ViewCount { get; set; }
        public string RegistMemberID { get; set; }
        public DateTime RegistDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public Int64 No { get; set; }
        public object Total { get; set; }
    }
}