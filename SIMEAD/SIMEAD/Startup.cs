using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SIMEAD.Startup))]
namespace SIMEAD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
