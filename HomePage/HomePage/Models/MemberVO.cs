using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomePage.Models
{
    public class MemberVO
    {
        public string MemberID { get; set; }
        public string MemberPWD { get; set; }
        public string MemberName { get;set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public DateTime EntryDate { get; set; }
        public int Code { get; set; }
        public object Total { get; set; }
        public object No { get; set; }
    }
}