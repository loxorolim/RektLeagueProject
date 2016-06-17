using RektLeague.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RektLeague.Repositories
{
    public class WebPostContext : DbContext
    {
        public WebPostContext()
        {

        }
        public DbSet<WebPost> WebPosts { get; set; }
        public DbSet<Element> Elements { get; set; }

    }
}