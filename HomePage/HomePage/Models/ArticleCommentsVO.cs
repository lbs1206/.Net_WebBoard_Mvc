using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomePage.Models
{
    public class ArticleCommentsVO
    {
        public int CommentID { get; set; }
        public int ArticleIDX { get; set; }
        public string MemberID { get; set; }
        public string Comments { get; set; }
        public DateTime RegistDate { get; set; }
        public Int64 No { get; set; }
        public object Total { get; set; }
    }
}