using RektLeague.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RektLeague.Repositories
{
    public class WebPostContextSeedData
    {
        public WebPostContextSeedData()
        {
            using (var db = new WebPostContext())
            {
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
}