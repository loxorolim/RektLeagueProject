using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RektLeague.App_Start;
using RektLeague.Models;
using RektLeague.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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

                byte[] imgBytes = Config.getFormFileBytes(model.Image);
                ApplicationUser toModify = userManager.FindByName(loggedUser);
                if (toModify != null)
                {
                    toModify.Image = imgBytes;
                    userManager.Update(toModify);

                    store.Context.SaveChanges();
                }
            }
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
    }
}