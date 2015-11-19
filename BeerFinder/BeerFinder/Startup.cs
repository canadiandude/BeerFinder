using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeerFinder.Startup))]
namespace BeerFinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
