using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FantaC.Startup))]
namespace FantaC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
