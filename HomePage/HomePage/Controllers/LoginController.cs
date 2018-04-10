using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomePage.Models;
using System.Data.Entity;

namespace HomePage.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            MemberVO member = new MemberVO();
            return View(member);
        }

        [HttpPost]
        public ActionResult Login(MemberVO member)
        {
            
            try
            {
                Login login = new Login();
                Boolean result = login.login(member.MemberID, member.MemberPWD);
                if (result)
                {
                    MemberVO mv = login.session(member.MemberID);
                    Session["ID"] = mv.MemberID;
                    Session["Code"] = mv.Code.ToString();
                    ViewBag.Result = "OK";
                }
                else
                {
                    ViewBag.Result = "FAIL";
                }
            }
            catch (Exception e)
            {
                ViewBag.Result = "FAIL";
            }
          
            return View(member);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return View();
        }

    }
}