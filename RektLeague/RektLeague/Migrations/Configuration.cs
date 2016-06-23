namespace RektLeague.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RektLeague.Repositories.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override async void Seed(RektLeague.Repositories.ApplicationDbContext db)
        {
            if (!db.Roles.Any(r => r.Name == "admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(db);
                await roleStore.CreateAsync(new IdentityRole { Name = "admin" });
            }
            if (!db.Users.Any())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var admin = new ApplicationUser { UserName = "Admin", Email = "loxorolim@gmail.com" };
                userManager.Create(admin, "V3ry!p4ssw0rd");
                userManager.AddToRole(admin.Id, "admin");
            }
            if (!db.WebPosts.Any())
            {
                byte[] image = System.IO.File.ReadAllBytes("C:\\Users\\Guilherme\\Source\\Repos\\RektLeague4.6.1\\RektLeague\\RektLeague\\Content\\Images\\heresdoge.jpg");
                byte[] gif = System.IO.File.ReadAllBytes("C:\\Users\\Guilherme\\Source\\Repos\\RektLeague4.6.1\\RektLeague\\RektLeague\\Content\\Images\\oraora.gif");
                var WebPost2 = new WebPost()
                {
                    Title = "GET BIRDED!!!",
                    PublicationDate = DateTime.UtcNow,
                    Elements = new List<Element>
                    {
                        new Element() {ElementType=Element.Type.Text, PostString="Very Test" },
                        new Element() {ElementType=Element.Type.Image, PostBytes = image },
                        new Element() {ElementType=Element.Type.Text, PostString="Much Wow" },
                        new Element() {ElementType=Element.Type.Image, PostBytes = gif },
                        new Element() {ElementType=Element.Type.YoutubeURL, PostString="https://www.youtube.com/embed/SRmj_VdLIL8"},
                    },
                    Author = "doge",
                };
                db.WebPosts.Add(WebPost2);
                db.Elements.AddRange(WebPost2.Elements);
                for (int i = 0; i < 20; i++)
                {
                    var WebPostTeste = new WebPost()
                    {
                        Title = "POSTTESTE" + i,
                        PublicationDate = DateTime.UtcNow,
                        Elements = new List<Element>
                        {
                            new Element() {ElementType=Element.Type.Text, PostString="OhMyGoooooood!"+i },
                            new Element() {ElementType=Element.Type.Text, PostString="Hueeee!"+i },
                        },
                    };
                    db.WebPosts.Add(WebPostTeste);
                    db.Elements.AddRange(WebPostTeste.Elements);
                }
                db.SaveChanges();
            }
        }
    }
}
