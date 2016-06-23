using RektLeague.App_Start;
using RektLeague.BusinessRules;
using RektLeague.Models;
using RektLeague.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RektLeague.Controllers
{
    public class WebPostController : Controller
    {

        private WebPostBR _webPostBr = new WebPostBR();

        public ActionResult WebPosts(int id)
        {
            ViewBag.Exhibit = (int)Config.ExhibitType.All;
            return View();
        }
        public ActionResult Category(int id)
        {
            ViewBag.Category = id;
            ViewBag.Exhibit = (int)Config.ExhibitType.Category;
            return View("WebPosts");
        }
        public ActionResult Author(string id)
        {
            ViewBag.Author = id;
            ViewBag.Exhibit = (int)Config.ExhibitType.Author;
            return View("WebPosts");
        }
        public ActionResult WebPost(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [Authorize(Roles = "admin")]
        public ActionResult WritePost()
        {
            ViewBag.CategoryNames = Config.categoryNames;
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public string Preview(WebPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                WebPost wp = _webPostBr.BuildWebPostFromViewModel(model);
                List<WebPost> wl = new List<WebPost>();
                wl.Add(wp);
                return _webPostBr.GetWebPostListJson(wl);
            }
            return "";

        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult WritePost(WebPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_webPostBr.AddWebPost(model))
                {
                    Response.StatusCode = (int)HttpStatusCode.Created;
                    return RedirectToAction("WebPosts", "WebPost");
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed");
        }
        [Authorize(Roles = "admin")]
        public ActionResult Remove(int id)
        {
            _webPostBr.RemoveWebPostById(id);
            return RedirectToAction("WebPosts", "WebPost");
        }
        public string WebPostJson(int webpostNumber)
        {
            try
            {
                return _webPostBr.GetWebPostJson(webpostNumber);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public string WebPostsJson(int webpostNumber, int category, string author, int exhibit)
        {
            try
            {
                switch(exhibit)
                {
                    case (int)Config.ExhibitType.All:
                        return _webPostBr.GetWebPostListJson(webpostNumber);
                    case (int)Config.ExhibitType.Category:
                        return _webPostBr.GetWebPostListJsonByCategory(webpostNumber, category);
                    case (int)Config.ExhibitType.Author:
                        return _webPostBr.GetWebPostListJsonByAuthor(webpostNumber, author);
                    default:
                        break;

                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }
    }
}