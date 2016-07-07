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
        public ActionResult Search(string search)
        {
            ViewBag.Search = search;
            ViewBag.Exhibit = (int)Config.ExhibitType.Search;
            return View("WebPosts");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult WritePost()
        {
            ViewBag.CategoryNames = Config.categoryNames;
            return View();
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        public string WebPostsJson(int webpostNumber, int category, string author, string search, int exhibit)
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
                    case (int)Config.ExhibitType.Search:
                        return _webPostBr.GetWebPostListJsonBySearchParameter(webpostNumber, search);
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