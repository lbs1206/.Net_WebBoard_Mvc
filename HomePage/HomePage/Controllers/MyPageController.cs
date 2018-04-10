using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomePage.Models;
using System.Data.Entity;

namespace HomePage.Controllers
{
    public class MyPageController : Controller
    {
        // GET: MyPage
        
        public JsonResult update(string memberid,string memberpwd,string membername,string email,string telephone)
        {
            string result = string.Empty;
            //dao
            Member member = new Member();
            //vo
            MemberVO mv = new MemberVO();
            mv.MemberID = memberid;
            mv.MemberPWD = memberpwd;
            mv.MemberName = membername;
            mv.Email = email;
            mv.Telephone = telephone;

            Boolean check = member.update(mv);
            if (check)
            {
                result = "OK";
            }
            else
            {
                result = "FAIL";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MyPage()
        {
            string s = Session["ID"].ToString();
            Member member = new Member();
            MemberVO mv = member.select(s);
            ViewBag.MemberID = mv.MemberID;
            ViewBag.MemberPWD = mv.MemberPWD;
            ViewBag.MemberName = mv.MemberName;
            ViewBag.Email = mv.Email;
            ViewBag.Telephone = mv.Telephone;
            return View(mv);
        }

        public JsonResult delete(string memberid)
        {
            string result = string.Empty;
            Member memberdao = new Member();
            Boolean check = memberdao.delete(memberid);

            if (check)
            {
                result = "OK";
                Session.Clear();
            }
            else
            {
                result = "FAIL";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}