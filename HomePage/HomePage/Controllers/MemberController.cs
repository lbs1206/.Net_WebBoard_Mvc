using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomePage.Models;
using System.Data.Entity;

namespace HomePage.Controllers
{
    public class MemberController : Controller
    {
        //객체모델을 통한 DB 데이터 처리를 위한 관련 DB 객체를 상단에 정의합니다.
        

        [HttpGet]
        public ActionResult Entry()
        {
            MemberVO member = new MemberVO();
            ViewBag.EmailPattern = "/^[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*.[a-zA-Z]{2,3}$/";
            return View(member);
        }

        [HttpPost]
        public ActionResult Entry(MemberVO member)
        {
            try
            {
                Member memberdao = new Member();
                Boolean check =memberdao.insert(member.MemberID, member.MemberPWD, member.MemberName, member.Email, member.Telephone);
                if (check)
                {
                    ViewBag.Result = "OK";
                }
                
                ViewBag.EmailPattern = "/^[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*.[a-zA-Z]{2,3}$/";
            }
            catch (Exception e)
            {
                ViewBag.Result = "FAIL";
            }
            //return RedirectToAction("List","Member");
            return View(member);
        }

        
        public JsonResult delete(string memberid)
        {
            string result = string.Empty;
            Member memberdao = new Member();
            Boolean check = memberdao.delete(memberid);

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

        public JsonResult IDCheck(string memberid)
        {
            string result = string.Empty;
            Member member = new Member();
            Boolean check = member.ID_Check(memberid);
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

        public ActionResult List(int pN,int start)
        {
            Member member = new Member();
            List<MemberVO> list = member.MemberList(pN);
           
            ViewBag.Total = list.First().Total;
            ViewBag.pN = pN;
            ViewBag.start = start;
            ViewBag.end = start+9;
            return View(list);
        }
    }
}