using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomePage.Models;
using System.Data.Entity;

namespace HomePage.Controllers
{
    public class BoardController : Controller 
    {
        //게시판 글 리스트
        // GET: Board
        public ActionResult List(int pN, int start)
        {
            Articles article = new Articles();
            List<ArticlesVO> list = article.ArticleList(pN);
            ViewBag.Total = list.First().Total;
            ViewBag.pN = pN;
            ViewBag.start = start;
            ViewBag.end = start + 9;
            return View(list);
        }

        //게시판 글 생성 View
        public ActionResult Create()
        {
            ViewBag.ID = Session["ID"];
            return View();
        }
        [HttpPost]
        public ActionResult Create(string Title,string Contents)
        {
            string result = "";
            string id = Session["ID"].ToString();
            Articles articles = new Articles();
            Boolean check = articles.Create(Title, Contents, id);
            if (check)
            {
                result = "OK";
            }
            else
            {
                result = "FAIL";
            }
            ViewBag.Result = result;
            ViewBag.ID = id;
            return View();
        }
        //게시판 글 수정 뷰
        [HttpGet]
        public ActionResult Update(string ArticleIDX,string No)
        {
            Articles ar = new Articles();
            ArticlesVO article = ar.Detail(No);
            ViewBag.ArticleIDX = ArticleIDX;//게시판 아이디
            ViewBag.Subject = article.Title;
            ViewBag.Contents = article.Contents;
            return View();
        }
        //게시판 글 수정 check
        [HttpPost]
        public ActionResult Update(string ArticleIDX, string Title, string Contents)
        {
            string result = string.Empty;
            Articles articles = new Articles();
            Boolean check = articles.Update(ArticleIDX, Session["ID"].ToString(), Title, Contents);
            if (check)
            {
                result = "OK";
            }
            else
            {
                result = "FAIL";
            }
            ViewBag.Result = result;
            return View();
        }
        //게시판 글 삭제
        public JsonResult delete(string ArticleIDX)
        {
            string result = string.Empty;
            Articles articles = new Articles();
            Boolean check = articles.Delete(ArticleIDX);
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
        //게시판 글 조회수
        public JsonResult ViewCount(string ArticleIDX, string ViewCount)
        {
            string result = string.Empty;
            Articles articles = new Articles();
            int VC = int.Parse(ViewCount) + 1;
            Boolean check = articles.ViewCount(ArticleIDX, VC);

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
        //게시판 글 단일 뷰
        public ActionResult Detail (string No,int pN,int start)
        {
            Articles ar = new Articles();
            ArticlesVO article = ar.Detail(No);
            List<ArticlesVO> list = ar.ArticleList(1);
            
            ViewBag.Max = list.First().Total;
            ViewBag.Contents = article.Contents;
            ViewBag.Subject = article.Title;
            ViewBag.ViewCount = article.ViewCount;
            ViewBag.RegistMemberID = article.RegistMemberID;
            ViewBag.RegistDate = article.RegistDate;
            ViewBag.No = article.No;
            ViewBag.ArticleIDX = article.ArticleIDX;
            ViewBag.sessionID = Session["ID"];
            
            //댓글

            ArticleComments ac = new ArticleComments();
            List<ArticleCommentsVO> CommentList = ac.CommentList(article.ArticleIDX, pN);
            if (CommentList.Count == 0)
            {
                ViewBag.Total = 0;
            }
            else
            {
                ViewBag.Total = CommentList.First().Total;
            }
            
            ViewBag.ArticleIDX = article.ArticleIDX;
            ViewBag.pN = pN;
            ViewBag.start = start;
            ViewBag.end = start + 9;
            return View(CommentList);
        }

        public ActionResult SearchDetail(string KeyWord,string No, int pN, int start)
        {
            Articles ar = new Articles();
            ArticlesVO article = ar.SearchDetail(KeyWord,No);
            List<ArticlesVO> list = ar.SearchList(1,KeyWord);

            ViewBag.Max = list.First().Total;
            System.Diagnostics.Debug.WriteLine("SearchDetail.Max : "+list.First().Total);
            ViewBag.Contents = article.Contents;
            ViewBag.Subject = article.Title;
            ViewBag.ViewCount = article.ViewCount;
            ViewBag.RegistMemberID = article.RegistMemberID;
            ViewBag.RegistDate = article.RegistDate;
            ViewBag.No = article.No;
            ViewBag.ArticleIDX = article.ArticleIDX;
            ViewBag.sessionID = Session["ID"];
            ViewBag.KeyWord = KeyWord;
            
            //댓글

            ArticleComments ac = new ArticleComments();
            List<ArticleCommentsVO> CommentList = ac.CommentList(article.ArticleIDX, pN);
            if (CommentList.Count == 0)
            {
                ViewBag.Total = 0;
            }
            else
            {
                ViewBag.Total = CommentList.First().Total;
            }

            ViewBag.ArticleIDX = article.ArticleIDX;
            ViewBag.pN = pN;
            ViewBag.start = start;
            ViewBag.end = start + 9;
            return View(CommentList);
        }


        //게시판 글 검색
        public ActionResult SearchPage(string KeyWord, int pN, int start)
        {
            Articles article = new Articles();
            List<ArticlesVO> list = article.SearchList(pN, KeyWord);

            ViewBag.Total = list.First().Total;
            ViewBag.pN = pN;
            ViewBag.start = start;
            ViewBag.end = start + 9;
            ViewBag.KeyWord = KeyWord;
            System.Diagnostics.Debug.WriteLine("Controller.SearchPage.KeyWord : " + KeyWord);
            ViewBag.Count = 1;
            
            return View(list);
        }

        public JsonResult SearchCheck(string KeyWord)
        {
            string result = string.Empty;
            Articles articles = new Articles();
            Boolean check = articles.SearchCheck(KeyWord);
            if (check)//검색어가 없을경우
            {
                result = "OK";
            }
            else//검색어가 있는 경우
            {
                result = "FAIL";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}