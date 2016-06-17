using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RektLeague.App_Start;
using RektLeague.Models;
using RektLeague.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RektLeague.BusinessRules
{
    public class WebPostBR
    {
        public bool AddWebPost(WebPostViewModel model)
        {
            WebPost newWebPost = BuildWebPostFromViewModel(model);
            try
            {
                using (var context = new WebPostContext())
                {
                    context.WebPosts.Add(newWebPost);
                    context.Elements.AddRange(newWebPost.Elements);
                    return SaveAll(context);
                }
            }
            catch (Exception ex)
            {

            }
            return false;

        }
        public WebPost BuildWebPostFromViewModel(WebPostViewModel model)
        {
            var newWebPost = new WebPost()
            {
                Title = model.Title,
                Author = model.Author,
                Category = model.Category,
                PublicationDate = model.PublicationDate,
                Elements = new List<Element>()
            };
            foreach (var el in model.DisplayOrder)
            {
                switch (el)
                {
                    case "Text":
                        newWebPost.Elements.Add(new Element() { PostString = model.Texts[0], ElementType = Element.Type.Text });
                        model.Texts.RemoveAt(0);
                        break;
                    case "Image/Gif":
                        newWebPost.Elements.Add(new Element() { PostBytes = getFormFileBytes(model.Images[0]), ElementType = Element.Type.Image });
                        model.Images.RemoveAt(0);
                        break;
                    case "YoutubeURL":
                        newWebPost.Elements.Add(new Element() { PostString = model.YoutubeURLs[0], ElementType = Element.Type.YoutubeURL });
                        model.YoutubeURLs.RemoveAt(0);
                        break;
                    default:
                        break;
                }
            }
            return newWebPost;
        }
        public string GetWebPostJson(int id)
        {
            try
            {
                using (var context = new WebPostContext())
                {
                    WebPost wb = context.WebPosts.First(i => i.Id == id);
                    List<WebPost> wpl = new List<WebPost>();
                    wpl.Add(wb);
                    return GetWebPostListJson(wpl);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string GetWebPostListJson(int initPos)
        {
            try
            {
                using (var context = new WebPostContext())
                {
                    IEnumerable<WebPost> wpl = context.WebPosts.OrderByDescending(t => t.PublicationDate).Skip(initPos).Take(Config.FetchSize).ToList();
                    return GetWebPostListJson(wpl);
                }                  
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string GetWebPostListJsonByCategory(int initPos, int category)
        {
            try
            {
                using (var context = new WebPostContext())
                {
                    IEnumerable<WebPost> wpl = context.WebPosts.Where(t => t.Category == category).OrderByDescending(t => t.PublicationDate).Skip(initPos).Take(Config.FetchSize).ToList();
                    return GetWebPostListJson(wpl);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string GetWebPostListJsonByAuthor(int initPos, string author)
        {
            try
            {
                using (var context = new WebPostContext())
                {
                    IEnumerable<WebPost> wpl = context.WebPosts.Where(t => t.Author == author).OrderByDescending(t => t.PublicationDate).Skip(initPos).Take(Config.FetchSize).ToList();
                    return GetWebPostListJson(wpl);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool ValidateWebPost(WebPost webpost)
        {
            //var newWebPost = Mapper.Map<WebPost>(model);

            return true;
        }
        public static byte[] getFormFileBytes(HttpPostedFileBase file)
        {
            byte[] data;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return data;
        }

        public void RemoveWebPostById(int id)
        {
            try
            {
                using (var context = new WebPostContext())
                {
                    WebPost wp = new WebPost() { Id = id };
                    context.WebPosts.Attach(wp);
                    context.WebPosts.Remove(wp);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public IEnumerable<WebPost> GetAllWebPosts()
        {
            try
            {
                using (var context = new WebPostContext())
                {
                    return context.WebPosts.OrderBy(t => t.PublicationDate).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool SaveAll(WebPostContext context)
        {
            return context.SaveChanges() > 0;
        }
        public IEnumerable<WebPost> GetWebPostsList(int startingPoint)
        {
            try
            {
                using (var context = new WebPostContext())
                {
                    IEnumerable<WebPost> wpl = context.WebPosts.OrderByDescending(t => t.PublicationDate).Skip(startingPoint).Take(10).ToList(); //MUDAR ESSE 10 DEPOIS
                    return wpl;
                }
            }
            catch (Exception ex)
            {
                // _logger.LogError("Could not get Web Posts List from database", ex);
                return null;
            }
        }
        public WebPost GetWebPostById(int id)
        {
            try
            {
                using (var context = new WebPostContext())
                {
                    WebPost wb = context.WebPosts.First(i => i.Id == id);
                    return wb;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Could not get Web Post from database", ex);
                return null;
            }
        }
        public string GetWebPostListJson(IEnumerable<WebPost> obj)
        {
            
            string json = JsonConvert.SerializeObject(obj,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            JArray WebPosts = JArray.Parse(json);
            foreach(var wb in WebPosts.Children())
            {
                var cat = (int) wb["category"];
                wb["categoryName"] = Config.categoryNames[cat];
                
            }
            return WebPosts.ToString();
            
           

            
           
        }

    }
   
}