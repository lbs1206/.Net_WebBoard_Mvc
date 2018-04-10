using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomePage.Models;
using System.Data.Entity;

namespace HomePage.Controllers
{
    public class ArticleCommentsController : Controller
    {
        // GET: ArticleCommnets
        public ActionResult CommentList(string ArticleIDX,string pageNum,string start)
        {
            ArticleComments ac = new ArticleComments();
           
            ViewBag.ArticleIDX = ArticleIDX;
            ViewBag.pN = pageNum;
            ViewBag.start = start;
            ViewBag.end = start + 9;
            return View();
        }
        public ActionResult Create()
        {
            string ID = Session["ID"].ToString();
            ViewBag.ID = ID;
            return View();
        }
        public JsonResult CommentCreate(string ArticleIDX, string Contents)
        {
            string result = string.Empty;
            ArticleComments ac = new ArticleComments();
            string ID = Session["ID"].ToString();
            Boolean check = ac.CommentCreate(ArticleIDX, Contents,ID);
            ViewBag.ID = ID;
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

        public JsonResult CommentDelete(int CommentID)
        {
            string result = string.Empty;

            ArticleComments ac = new ArticleComments();
            Boolean check = ac.CommentDelete(CommentID);
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

        public ActionResult CommentUpdateView(int CommentID)
        {
            ArticleComments ac = new ArticleComments();
            ArticleCommentsVO acvo = ac.OneSelect(CommentID);
            ViewBag.Comments = acvo.Comments;
            ViewBag.CommentID = acvo.CommentID;
            return View();
        }

        public JsonResult UpdateCheck(string Comment, int CommentID)
        {
            string result = string.Empty;
            ArticleComments ac = new ArticleComments();
            Boolean check = ac.Update(Comment,CommentID);
            if (check)
            {
                result = "OK";
            }
            else
            {
                result = "FAIL";
            }


            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}