using Microsoft.Owin;
using Owin;
using RektLeague.Repositories;

[assembly: OwinStartupAttribute(typeof(RektLeague.Startup))]
namespace RektLeague
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
            //WebPostContextSeedData seed = new WebPostContextSeedData();
            //seed.SeedData();
            //await seeder.EnsureSeedDataAsync();
        }
    }
}
