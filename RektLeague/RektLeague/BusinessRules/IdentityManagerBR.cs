using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace RektLeague.BusinessRules
{
    public class IdentityManagerBR
    {
        public void ModifyUser(UserSettingsViewModel model, string loggedUser)
        {
            using (var context = new ApplicationDbContext())
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                
                ApplicationUser toModify = userManager.FindByName(loggedUser);
                if (toModify != null)
                {
                    if(ModifyUserName(toModify,model.DisplayName) | ModifyUserImage(toModify,model.Image))
                    {
                        userManager.Update(toModify);
                        store.Context.SaveChanges();
                    }
                }
            }
        }
        public bool ModifyUserName(ApplicationUser user, string newDisplayName)
        {
            if (newDisplayName != null)
            {
                user.DisplayName = newDisplayName;
                return true;
            }
            return false;
        }
        public bool ModifyUserImage(ApplicationUser user, HttpPostedFileBase image)
        {
            if (image != null)
            {
                byte[] imgBytes = Config.getFormFileBytes(image);
                user.Image = imgBytes;
                return true;
            }
            return false;

        }
        public static string GetUserByteArrayBase64String(string username)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var store = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(store);
                    ApplicationUser user = userManager.FindByName(username);
                    return Config.getByteArrayBase64String(user.Image);
                }
            }
            catch(Exception ex)
            {

            }
            return String.Empty;

        }
        public static string GetUserDisplayName(string username)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var store = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(store);
                    ApplicationUser user = userManager.FindByName(username);
                    return user.DisplayName;
                }
            }
            catch (Exception ex)
            {

            }
            return String.Empty;

        }
        public string GetUsersListJson(int currentPage, int numberToFetch, string order, string orientation, string searchParameter, List<string> adminFilter)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    IQueryable<ApplicationUser> ul = context.Users;
                    //List<Expression<Func<ApplicationUser, bool>>> expressions = null;
                    //Expression combined = Expression.OrElse(firstPredicate.Body,
                    //                            secondPredicate.Body);
                    Expression<Func<ApplicationUser, bool>> combined = null;
                    //foreach (string role in adminFilter)
                    //{
                    //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                    //    var r = roleManager.FindByName(role);
                    //    //expressions.Add(t => t.Roles.Select(y => y.RoleId).Contains(r.Id));
                    //    Expression<Func<ApplicationUser, bool>> exp = t => t.Roles.Select(y => y.RoleId).Contains(r.Id);
                    //    //combined = Expression.Or(combined, exp);
                    //}
                    //var lambda = Expression.Lambda<Func<ApplicationUser, bool>>(combined);
                    //ul = ul.Where(lambda);
                    if (searchParameter != null)
                    {
                        ul = ul.Where(t => t.UserName.Contains(searchParameter) ||
                                                      t.Email.Contains(searchParameter) ||
                                                      t.DisplayName.Contains(searchParameter));
                    }
   
                        

                    if (order.Equals("unordered"))
                    {
                        ul = ul.OrderBy(t => t.UserName);
                    }
                    if(order.Equals("login"))
                    {
                        if (orientation.Equals("ascending"))
                            ul = ul.OrderBy(t => t.UserName);
                        else if (orientation.Equals("descending"))
                            ul = ul.OrderByDescending(t => t.UserName);
                    }
                    else if (order.Equals("displayName"))
                    {
                        if (orientation.Equals("ascending"))
                            ul = ul.OrderBy(t => t.DisplayName);
                        else if (orientation.Equals("descending"))
                            ul = ul.OrderByDescending(t => t.DisplayName);
                    }
                    else if (order.Equals("email"))
                    {
                        if (orientation.Equals("ascending"))
                            ul = ul.OrderBy(t => t.Email);
                        else if (orientation.Equals("descending"))
                            ul = ul.OrderByDescending(t => t.Email);
                    }
                    ul = ul.Skip((currentPage - 1) * numberToFetch).Take(numberToFetch);
                    var wow = ul.ToList();
                    int totalUsers = context.Users.Count();
                    return BuildUsersListJson(ul.ToList(),totalUsers);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string BuildUsersListJson(List<ApplicationUser> obj, int totalUsers)
        {

            string json = JsonConvert.SerializeObject(obj,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            JArray Users = JArray.Parse(json);
            for(int i=0;i< Users.Children().Count();i++)
            {
                string val = "";
                if (UserIsInRole(obj.ElementAt(i), "SuperAdmin"))
                    val = "SuperAdmin";
                else if (UserIsInRole(obj.ElementAt(i), "Admin"))
                    val = "Admin";
                else
                    val = "User";
                Users.Children().ElementAt(i)["adminRole"] = val;
            }
            JObject jo = new JObject(
                               new JProperty("totalUsers", totalUsers),
                               new JProperty("users", Users));
            return jo.ToString();
        }
        public bool UserIsInRole(ApplicationUser u,string role)
        {
            using (var context = new ApplicationDbContext())
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);
                return userManager.IsInRole(u.Id, role);
            }
        }
        public int GetTotalUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Users.Count();
            }
        }
        public void DeleteUser(string username)
        {
            using (var context = new ApplicationDbContext())
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);
                ApplicationUser user = userManager.FindByName(username);
                userManager.Delete(user);
            }
        }
        public void ChangeUserRole(string username, string newrole)
        {
            using (var context = new ApplicationDbContext())
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);
                ApplicationUser user = userManager.FindByName(username);
                foreach (var role in userManager.GetRoles(user.Id))
                {
                    userManager.RemoveFromRole(user.Id, role);
                }
                if(newrole == "Admin")
                    userManager.AddToRole(user.Id, newrole);
            }
        }
    }

}