using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Turnierplaner.Startup))]
namespace Turnierplaner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
